using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.NutritionRepository.Abstract;
using FitApp.NutritionRepository.Model;
using FitApp.NutritionRepository.Settings;
using Nest;

namespace FitApp.NutritionRepository
{
    public class NutritionRepository : GenericRepository<Nutrition, Guid>, INutritionRepository
    {
        public NutritionRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(
            elasticClient,
            settings)
        {
        }

        public async Task<List<Nutrition>> GetAll()
        {
            var setList = new List<Nutrition>();

            var searchDescriptor = new SearchDescriptor<Nutrition>()
                .Index(IndexName)
                .Take(1000)
                .Query(q => q.MatchAll())
                .Scroll("2m");

            var result = await SessionClient.SearchAsync<Nutrition>(searchDescriptor);
            if (result.Documents != null && result.Documents.Any())
            {
                setList.AddRange(result.Documents);
            }

            var scrollId = result.ScrollId;
            while (!string.IsNullOrEmpty(scrollId))
            {
                List<Nutrition> activities;
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

        public Task<Nutrition> GetNutritionByNameAsync(string nutritionName)
        {
            if (string.IsNullOrEmpty(nutritionName)) throw new ArgumentNullException(nameof(nutritionName));
            var result = SessionClient.SearchAsync<Nutrition>(s => s
                .Take(1)
                .Query(x => x
                    .Term(m => m
                        .Field(f => f.Name)
                        .Value(nutritionName)))
                .Index(IndexName)).GetAwaiter().GetResult();

            HandleResult(result);
            return Task.FromResult(result.Documents.FirstOrDefault());
        }

        public Task<List<Nutrition>> GetNutritionsByIdList(List<Guid> nutritionIds)
        {
            if (nutritionIds == null || !nutritionIds.Any()) throw new ArgumentNullException(nameof(nutritionIds));

            var searchDescriptor = new SearchDescriptor<Nutrition>().Index(IndexName);

            searchDescriptor.Query(x => x
                .Terms(m => m
                    .Field(f => f.Id.Suffix("keyword"))
                    .Terms(nutritionIds)));


            var result = SessionClient.SearchAsync<Nutrition>(searchDescriptor).GetAwaiter().GetResult();

            HandleResult(result);
            return Task.FromResult(result.Documents.ToList());
        }
    }
}