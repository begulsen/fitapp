using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.ActivityRepository.Abstract;
using FitApp.ActivityRepository.Model;
using FitApp.MealNutritionRepository.Abstract;
using FitApp.MealNutritionRepository.Model;
using FitApp.MealRepository.Abstract;
using FitApp.MealRepository.Model;
using FitApp.MenuRepository.Abstract;
using FitApp.MenuRepository.Model;
using FitApp.NutritionRepository.Abstract;
using FitApp.NutritionRepository.Model;
using FitApp.SetRepository.Abstract;
using FitApp.SetRepository.Model;
using FitApp.TrainingRepository.Abstract;
using FitApp.UserPrivateDietDetailRepository.Abstract;
using FitApp.UserPrivateDietDetailRepository.Model;
using FitApp.UserPrivateDietRepository.Abstract;
using FitApp.UserPrivateDietRepository.Model;
using FitApp.UserPrivateTrainingDetailRepository.Abstract;
using FitApp.UserPrivateTrainingDetailRepository.Model;
using FitApp.UserPrivateTrainingRepository.Abstract;
using FitApp.UserPrivateTrainingRepository.Model;
using FitApp.UserRepository.Abstract;
using FitApp.UserRepository.Model;

namespace FitApp.Api.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly ISetRepository _setRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly INutritionRepository _nutritionRepository;
        private readonly IMealNutritionRepository _mealNutritionRepository;
        private readonly IMealRepository _mealRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IUserPrivateTrainingRepository _userPrivateTrainingRepository;
        private readonly IUserPrivateTrainingDetailRepository _userPrivateTrainingDetailRepository;
        private readonly IUserPrivateDietRepository _userPrivateDietRepository;
        private readonly IUserPrivateDietDetailRepository _userPrivateDietDetailRepository;
        
        public ApplicationService(IUserRepository userRepository, ITrainingRepository trainingRepository, ISetRepository setRepository, 
            IActivityRepository activityRepository, INutritionRepository nutritionRepository, IMealNutritionRepository mealNutritionRepository, 
            IMealRepository mealRepository, IMenuRepository menuRepository, IUserPrivateTrainingRepository userPrivateTrainingRepository, 
            IUserPrivateTrainingDetailRepository userPrivateTrainingDetailRepository, IUserPrivateDietRepository userPrivateDietRepository, IUserPrivateDietDetailRepository userPrivateDietDetailRepository)
        {
            _userRepository = userRepository;
            _trainingRepository = trainingRepository;
            _setRepository = setRepository;
            _activityRepository = activityRepository;
            _nutritionRepository = nutritionRepository;
            _mealNutritionRepository = mealNutritionRepository;
            _mealRepository = mealRepository;
            _menuRepository = menuRepository;
            _userPrivateTrainingRepository = userPrivateTrainingRepository;
            _userPrivateTrainingDetailRepository = userPrivateTrainingDetailRepository;
            _userPrivateDietRepository = userPrivateDietRepository;
            _userPrivateDietDetailRepository = userPrivateDietDetailRepository;
        }
        
        public async Task<User> GetUser(Guid customerId)
        {
            User user = await _userRepository.GetAsync(customerId.ToString());
            return user;
        }

        public async Task<User> GetUserByMail(string customerMail)
        {
            User user = await _userRepository.GetUserByMail(customerMail);
            return user;
        }

        public async Task<Guid> CreateUser(User user)
        {
            await _userRepository.SaveAsync(user);
            return user.Id;
        }

        public async Task DeleteUser(Guid userId)
        {
            await _userRepository.DeleteAsync(userId);
        }

        public async Task DeleteUserPrivateDiet(Guid id)
        {
            await _userPrivateDietRepository.DeleteAsync(id);
        }

        public async Task DeleteUserPrivateDietDetail(Guid id)
        {
            await _userPrivateDietDetailRepository.DeleteAsync(id);
        }

        public async Task DeleteUserPrivateTraining(Guid id)
        {
            await _userPrivateTrainingRepository.DeleteAsync(id);
        }

        public async Task DeleteUserPrivateTrainingDetail(Guid id)
        {
            await _userPrivateTrainingDetailRepository.DeleteAsync(id);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.SaveAsync(user);
        }

        public async Task<Guid> CreateTraining(TrainingRepository.Model.Training training)
        {
            await _trainingRepository.SaveAsync(training);
            return training.Id;
        }

        public async Task<TrainingRepository.Model.Training> GetTraining(string trainingName)
        {
            TrainingRepository.Model.Training training = await _trainingRepository.GetTrainingByNameAsync(trainingName);
            return training;
        }

        public async Task UpdateTraining(Guid trainingId,  List<Guid> setIds)
        {
            await _trainingRepository.Update(trainingId, setIds);
        }

        public async Task CreateSet(Set set)
        {
            await _setRepository.SaveAsync(set);
        }

        public async Task CreateActivity(Activity activity)
        {
            await _activityRepository.SaveAsync(activity);
        }

        public async Task<Set> GetSet(Guid setId)
        {
            return await _setRepository.GetAsync(setId.ToString());
        }

        public async Task UpdateSet(Set set)
        {
            await _setRepository.SaveAsync(set);
        }

        public async Task<List<Set>> GetAllSets()
        {
            List<Set> sets = await _setRepository.GetAll();
            return sets;
        }

        public async Task<Activity> GetActivityByName(string activityName)
        {
            Activity activity = await _activityRepository.GetActivityByNameAsync(activityName);
            return activity;
        }

        public async Task CreateNutrition(Nutrition nutrition)
        {
            await _nutritionRepository.SaveAsync(nutrition);
        }

        public async Task<Nutrition> GetNutrition(Guid nutritionId)
        {
            Nutrition nutrition = await _nutritionRepository.GetAsync(nutritionId.ToString());
            return nutrition;
        }

        public async Task UpdateNutrition(Nutrition nutrition)
        {
            await _nutritionRepository.SaveAsync(nutrition);
        }

        public async Task CreateMealNutritionModel(MealNutrition mealNutrition)
        {
            await _mealNutritionRepository.SaveAsync(mealNutrition);
        }

        public async Task<MealNutrition> GetMealNutritionByNutritionId(Guid nutritionId)
        {
            MealNutrition mealNutrition = await _mealNutritionRepository.GetByNutritionIdAsync(nutritionId);
            return mealNutrition;
        }

        public async Task<List<MealNutrition>> GetMealNutritions(List<Guid> mealNutritionId)
        {
            List<MealNutrition> mealNutritions = await _mealNutritionRepository.GetMealNutritionsAsync(mealNutritionId);
            return mealNutritions;
        }

        public async Task CreateMeal(Meal meal)
        {
            await _mealRepository.SaveAsync(meal);
        }

        public async Task<Meal> GetMealByName(string mealName)
        {
            Meal meal = await _mealRepository.GetMealByNameAsync(mealName);
            return meal;
        }
        
        public async Task CreateMenu(string name, List<Day> days)
        {
            await _menuRepository.SaveAsync(new Menu()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Days = days,
                CreatedAt = DateTime.Now
            });
        }

        public async Task<Menu> GetMenuByName(string menuName)
        {
            Menu menu = await _menuRepository.GetMenuByNameAsync(menuName);
            return menu;
        }

        public async Task<List<Meal>> GetMeals(List<Guid> mealIdList)
        {
            List<Meal> meals = await _mealRepository.GetMealsByIdList(mealIdList);
            return meals;
        }

        public async Task<List<MealNutrition>> GetAllMealNutritions()
        {
            List <MealNutrition> mealNutritions = await _mealNutritionRepository.GetAll();
            return mealNutritions;
        }

        public async Task<List<Nutrition>> GetNutritionsByIdList(List<Guid> nutritionIds)
        {
            List<Nutrition> nutritions = await _nutritionRepository.GetNutritionsByIdList(nutritionIds);
            return nutritions;
        }

        public async Task<List<TrainingRepository.Model.Training>> GetAllTrainings()
        {
            List<TrainingRepository.Model.Training> trainings = await _trainingRepository.GetAll();
            return trainings;
        }

        public async Task<List<Menu>> GetAllMenus()
        {
            List<Menu> menus = await _menuRepository.GetAll();
            return menus;
        }

        public async Task<Set> GetSetByName(string setName)
        {
            Set set = await _setRepository.GetSetByName(setName);
            return set;        
        }

        public async Task<Activity> GetActivity(Guid activityId)
        {
            Activity activity = await _activityRepository.GetAsync(activityId.ToString());
            return activity;
        }

        public async Task<List<Activity>> GetAllActivities(List<string> equipments = null, string effectiveZone = null)
        {
            List<Activity> activites = await _activityRepository.GetAll(equipments, effectiveZone);
            return activites;
        }

        public async Task CreateUserPrivateTraining(UserPrivateTraining userPrivateTraining)
        {
            await _userPrivateTrainingRepository.SaveAsync(userPrivateTraining);
        }

        public async Task<UserPrivateTraining> GetUserPrivateTraining(Guid userId)
        {
            UserPrivateTraining model = await _userPrivateTrainingRepository.GetAsync(userId.ToString());
            return model;
        }

        public async Task<UserPrivateTrainingDetail> GetUserPrivateTrainingDetail(Guid userId)
        {
            UserPrivateTrainingDetail model = await _userPrivateTrainingDetailRepository.GetAsync(userId.ToString());
            return model;
        }

        public async Task CreateUserPrivateTrainingDetail(UserPrivateTrainingDetail userPrivateTrainingDetail)
        {  
            await _userPrivateTrainingDetailRepository.SaveAsync(userPrivateTrainingDetail);
        }

        public async Task<UserPrivateDiet> GetUserPrivateDiet(Guid userId)
        {
            UserPrivateDiet userPrivateDiet = await _userPrivateDietRepository.GetAsync(userId.ToString());
            return userPrivateDiet;
        }

        public async Task CreateUserPrivateDiet(UserPrivateDiet userPrivateDiet)
        {
            await _userPrivateDietRepository.SaveAsync(userPrivateDiet);
        }

        public async Task<UserPrivateDietDetail> GetUserPrivateDietDetail(Guid userId)
        {
            UserPrivateDietDetail model = await _userPrivateDietDetailRepository.GetAsync(userId.ToString());
            return model;
        }

        public async Task CreateUserPrivateDietDetail(UserPrivateDietDetail userPrivateDietDetail)
        {
            await _userPrivateDietDetailRepository.SaveAsync(userPrivateDietDetail);
        }

        public async Task<Nutrition> GetNutritionByName(string nutritionName)
        {
            Nutrition nutrition = await _nutritionRepository.GetNutritionByNameAsync(nutritionName);
            return nutrition;
        }
    }
}