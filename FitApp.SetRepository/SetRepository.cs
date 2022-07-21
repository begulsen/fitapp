using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.SetRepository.Abstract;
using FitApp.SetRepository.Model;
using FitApp.SetRepository.Settings;
using Nest;

namespace FitApp.SetRepository
{
    public class SetRepository : GenericRepository<Set, Guid>, ISetRepository
    {
        public SetRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient,
            settings)
        {
        }

        public async Task<List<Set>> GetAll()
        {
            var setList = new List<Set>();

            var searchDescriptor = new SearchDescriptor<Set>()
                .Index(IndexName)
                .Take(1000)
                .Query(q => q.MatchAll())
                .Scroll("2m");

            var result = await SessionClient.SearchAsync<Set>(searchDescriptor);
            if (result.Documents != null && result.Documents.Any())
            {
                setList.AddRange(result.Documents);
            }

            var scrollId = result.ScrollId;
            while (!string.IsNullOrEmpty(scrollId))
            {
                List<Set> sets;
                (sets, scrollId) = await ScrollAsync(scrollId);
                if (sets != null && sets.Any())
                {
                    setList.AddRange(sets);
                }
                else
                    break;
            }

            return setList;
        }
        public Task<Set> GetSetByName(string setName)
        {
            if (string.IsNullOrEmpty(setName)) throw new ArgumentNullException(nameof(setName));
            var result = SessionClient.SearchAsync<Set>(s => s
                .Take(1)
                .Query(x => x
                    .Term(m => m
                        .Field(f => f.Name.Suffix("keyword"))
                        .Value(setName)))
                .Index(IndexName)).GetAwaiter().GetResult();

            HandleResult(result);
            return Task.FromResult(result.Documents.FirstOrDefault());
        }
    }
}