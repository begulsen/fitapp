using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FitApp.MealRepository.Abstract;
using FitApp.MealRepository.Model;
using FitApp.MealRepository.Settings;
using Nest;

namespace FitApp.MealRepository
{
    public class MealRepository : GenericRepository<Meal, Guid>, IMealRepository
    {
        public MealRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient,
            settings)
        {
        }

        public async Task<List<Meal>> GetAll()
        {
            var mealList = new List<Meal>();

            var searchDescriptor = new SearchDescriptor<Meal>()
                .Index(IndexName)
                .Take(1000)
                .Query(q => q.MatchAll())
                .Scroll("2m");

            var result = await SessionClient.SearchAsync<Meal>(searchDescriptor);
            if (result.Documents != null && result.Documents.Any())
            {
                mealList.AddRange(result.Documents);
            }

            var scrollId = result.ScrollId;
            while (!string.IsNullOrEmpty(scrollId))
            {
                List<Meal> meals;
                (meals, scrollId) = await ScrollAsync(scrollId);
                if (meals != null && meals.Any())
                {
                    mealList.AddRange(meals);
                }
                else
                    break;
            }

            return mealList;
        }
        
        public Task<Meal> GetMealByNameAsync(string mealName)
        {
            if (string.IsNullOrEmpty(mealName)) throw new ArgumentNullException(nameof(mealName));
            var result = SessionClient.SearchAsync<Meal>(s => s
                .Take(1)
                .Query(x => x
                    .Term(m => m
                        .Field(f => f.Name.Suffix("keyword"))
                        .Value(mealName)))
                .Index(IndexName)).GetAwaiter().GetResult();

            HandleResult(result);
            return Task.FromResult(result.Documents.FirstOrDefault());
        }

        public Task<List<Meal>> GetMealsByIdList(List<Guid> mealIdList)
        {
            if (mealIdList == null || !mealIdList.Any()) throw new ArgumentNullException(nameof(mealIdList));
            var mealList = new List<Meal>();
            
            var searchDescriptor = new SearchDescriptor<Meal>().Index(IndexName);

            searchDescriptor.Query(x => x
                .Terms(m => m
                    .Field(f => f.Id.Suffix("keyword")).Terms(mealIdList.Distinct().ToList())));

            IElasticClient elasticClient = new ElasticClient();
            var jsonString = elasticClient.SourceSerializer.SerializeToString(searchDescriptor);

            var result = SessionClient.SearchAsync<Meal>(searchDescriptor).GetAwaiter().GetResult();

            HandleResult(result);
            if (result.Documents != null && result.Documents.Any())
            {
                mealList.AddRange(result.Documents);
            }
            return Task.FromResult(mealList);
        }
    }
}