using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.MealNutritionRepository.Abstract;
using FitApp.MealNutritionRepository.Model;
using FitApp.MealNutritionRepository.Settings;
using Nest;

namespace FitApp.MealNutritionRepository
{
    public class MealNutritionRepository: GenericRepository<MealNutrition, Guid>, IMealNutritionRepository
    {
        public MealNutritionRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient, settings) { }
        
        public async Task<List<MealNutrition>> GetAll()
        {
            var mealNutritionList = new List<MealNutrition>();

             var searchDescriptor = new SearchDescriptor<MealNutrition>()
                 .Index(IndexName)
                 .Take(1000)
                 .Query(q => q.MatchAll())
                 .Scroll("2m");
             
             var result = await SessionClient.SearchAsync<MealNutrition>(searchDescriptor);
             if (result.Documents != null && result.Documents.Any())
             {
                 mealNutritionList.AddRange(result.Documents);
             }
             var scrollId = result.ScrollId;
             while (!string.IsNullOrEmpty(scrollId))
             {
                 List<MealNutrition> mealNutritions;
                 (mealNutritions, scrollId) = await ScrollAsync(scrollId);
                 if (mealNutritions != null && mealNutritions.Any())
                 {
                     mealNutritionList.AddRange(mealNutritions);
                 }
                 else
                     break;
             }
             return mealNutritionList;
        }
        
        public Task<MealNutrition> GetByNutritionIdAsync(Guid nutritionId)
        {
            if (default == nutritionId) throw new ArgumentNullException(nameof(nutritionId));

            var searchDescriptor = new SearchDescriptor<MealNutrition>().Index(IndexName).Take(1);

            searchDescriptor.Query(x => x
                .Term(m => m
                    .Field(f => f.NutritionId.Suffix("keyword"))
                    .Value(nutritionId)));

            var result = SessionClient.SearchAsync<MealNutrition>(searchDescriptor).GetAwaiter().GetResult();
            HandleResult(result);
            return Task.FromResult(result.Documents.FirstOrDefault());
        }

        public Task<List<MealNutrition>> GetMealNutritionsAsync(List<Guid> mealNutritionIds)
        {
            if (mealNutritionIds == null || !mealNutritionIds.Any()) throw new ArgumentNullException(nameof(mealNutritionIds));

            var searchDescriptor = new SearchDescriptor<MealNutrition>().Index(IndexName);

            searchDescriptor.Query(x => x
                .Terms(m => m
                    .Field(f => f.Id.Suffix("keyword"))
                    .Terms(mealNutritionIds)));


            var result = SessionClient.SearchAsync<MealNutrition>(searchDescriptor).GetAwaiter().GetResult();
            HandleResult(result);
            return Task.FromResult(result.Documents.ToList());
        }
    }
}