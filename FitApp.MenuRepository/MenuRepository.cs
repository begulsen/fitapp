using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.ActivityRepository.Settings;
using FitApp.MenuRepository.Abstract;
using FitApp.MenuRepository.Model;
using Nest;

namespace FitApp.MenuRepository
{
    public class MenuRepository : GenericRepository<Menu, Guid>, IMenuRepository
    {
        public MenuRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient,
            settings)
        {
        }

        public async Task<List<Menu>> GetAll()
        {
            var setList = new List<Menu>();

            var searchDescriptor = new SearchDescriptor<Menu>()
                .Index(IndexName)
                .Take(1000)
                .Query(q => q.MatchAll())
                .Scroll("2m");

            var result = await SessionClient.SearchAsync<Menu>(searchDescriptor);
            if (result.Documents != null && result.Documents.Any())
            {
                setList.AddRange(result.Documents);
            }

            var scrollId = result.ScrollId;
            while (!string.IsNullOrEmpty(scrollId))
            {
                List<Menu> activities;
                (activities, scrollId) = await ScrollAsync(scrollId);
                if (activities != null && activities.Any())
                {
                    setList.AddRange(activities);
                }
                else
                    break;
            }

            return setList;
        }

        public Task<Menu> GetMenuByNameAsync(string menuName)
        {
            if (string.IsNullOrEmpty(menuName)) throw new ArgumentNullException(nameof(menuName));
            var result = SessionClient.SearchAsync<Menu>(s => s
                .Take(1)
                .Query(x => x
                    .Term(m => m
                        .Field(f => f.Name.Suffix("keyword"))
                        .Value(menuName)))
                .Index(IndexName)).GetAwaiter().GetResult();

            HandleResult(result);
            return Task.FromResult(result.Documents.FirstOrDefault());
        }
    }
}