using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.ActivityRepository.Model;
using FitApp.MealNutritionRepository.Model;
using FitApp.MealRepository.Model;
using FitApp.MenuRepository.Model;
using FitApp.NutritionRepository.Model;
using FitApp.SetRepository.Model;
using FitApp.UserPrivateDietDetailRepository.Model;
using FitApp.UserPrivateDietRepository.Model;
using FitApp.UserPrivateTrainingDetailRepository.Model;
using FitApp.UserPrivateTrainingRepository.Model;
using FitApp.UserRepository.Model;

namespace FitApp.Api.Service
{
    public interface IApplicationService
    {
        Task<User> GetUser(Guid customerId);
        Task<User> GetUserByMail(String customerMail);
        Task<Guid> CreateUser(User user);
        Task DeleteUser(Guid id);
        Task DeleteUserPrivateDiet(Guid id);
        Task DeleteUserPrivateDietDetail(Guid id);
        Task DeleteUserPrivateTraining(Guid id);
        Task DeleteUserPrivateTrainingDetail(Guid id);
        Task UpdateUser(User updateUser);
        Task<Guid> CreateTraining(TrainingRepository.Model.Training training);
        Task<List<TrainingRepository.Model.Training>> GetTrainings(List<string> trainingNames);
        Task UpdateTraining(Guid trainingId, List<Guid> setIds);
        Task CreateSet(Set toCreateSet);
        Task CreateActivity(Activity activity);
        Task<Set> GetSet(Guid setId);
        Task UpdateSet(Set toUpdateSet);
        Task<List<Set>> GetAllSets();
        Task<Activity> GetActivityByName(string activityName);
        Task CreateNutrition(Nutrition toCreateNutrition);
        Task<Nutrition> GetNutritionByName(string nutritionName);
        Task<Nutrition> GetNutrition(Guid nutritionId);
        Task UpdateNutrition(Nutrition toUpdateNutrition);
        Task CreateMealNutritionModel(MealNutrition toCreateMealNutrition);
        Task<MealNutrition> GetMealNutritionByNutritionId(Guid nutritionId);
        Task<List<MealNutrition>> GetMealNutritions(List<Guid> mealNutritionId);
        Task CreateMeal(Meal meal);
        Task<Meal> GetMealByName(string mealName);
        Task CreateMenu(string name, List<Day> days);
        Task<Menu> GetMenuByName(string menuName);
        Task<List<Meal>> GetMeals(List<Guid> guidList);
        Task<List<MealNutrition>> GetAllMealNutritions();
        Task<List<Nutrition>> GetNutritionsByIdList(List<Guid> nutritionIds);
        Task<List<TrainingRepository.Model.Training>> GetAllTrainings();
        Task<List<Menu>> GetAllMenus();
        Task<Set> GetSetByName(string setName);
        Task<Activity> GetActivity(Guid activityId);
        Task<List<Activity>> GetAllActivities(List<string> equipments = null, string effectiveZone = null);
        Task CreateUserPrivateTraining(UserPrivateTraining toUserPrivateTraining);
        Task<UserPrivateTraining> GetUserPrivateTraining(Guid userId);
        Task<UserPrivateTrainingDetail> GetUserPrivateTrainingDetail(Guid userId);
        Task CreateUserPrivateTrainingDetail(UserPrivateTrainingDetail userPrivateTrainingDetail);
        Task<UserPrivateDiet> GetUserPrivateDiet(Guid userId);
        Task CreateUserPrivateDiet(UserPrivateDiet toUserPrivateDiet);
        Task<UserPrivateDietDetail> GetUserPrivateDietDetail(Guid userId);
        Task CreateUserPrivateDietDetail(UserPrivateDietDetail p0);
    }
}