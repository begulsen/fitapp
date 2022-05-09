using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.MealNutritionRepository.Model;

namespace FitApp.MealNutritionRepository.Abstract
{
    public interface IMealNutritionRepository : IGenericRepository<MealNutrition>
    {
        Task<List<MealNutrition>> GetAll();
        Task<MealNutrition> GetByNutritionIdAsync(Guid nutritionId);
        Task<List<MealNutrition>> GetMealNutritionsAsync(List<Guid> mealNutritionId);
    }
}