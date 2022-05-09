using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.ActivityRepository.Abstract;
using FitApp.MealRepository.Model;

namespace FitApp.MealRepository.Abstract
{
    public interface IMealRepository : IGenericRepository<Meal>
    {
        Task<List<Meal>> GetAll();
        Task<Meal> GetMealByNameAsync(string activityName);
        Task<List<Meal>> GetMealsByIdList(List<Guid> guidList);
    }
}