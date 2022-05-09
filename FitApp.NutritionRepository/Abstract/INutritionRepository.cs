using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.NutritionRepository.Model;

namespace FitApp.NutritionRepository.Abstract
{
    public interface INutritionRepository  : IGenericRepository<Nutrition>
    {
        Task<List<Nutrition>> GetAll();
        Task<Nutrition> GetNutritionByNameAsync(string activityName);
        Task<List<Nutrition>> GetNutritionsByIdList(List<Guid> nutritionIds);
    }
}