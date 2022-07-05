using System;
using FitApp.ActivityRepository.Model;
using FitApp.Api.Controllers.ActivityController.Model;
using FitApp.Api.Controllers.MealController;
using FitApp.Api.Controllers.MealNutritionController.Model;
using FitApp.Api.Controllers.NutritionController.Model;
using FitApp.Api.Controllers.SetController.Model;
using FitApp.Api.Controllers.TrainingController.Model;
using FitApp.Api.Controllers.UserController.Model;
using FitApp.Api.Controllers.UserPrivateDietController.Model;
using FitApp.Api.Controllers.UserPrivateTrainingController.Model;
using FitApp.MealNutritionRepository.Model;
using FitApp.MealRepository.Model;
using FitApp.NutritionRepository.Model;
using FitApp.SetRepository.Model;
using FitApp.UserPrivateDietRepository.Model;
using FitApp.UserPrivateTrainingRepository.Model;
using FitApp.UserRepository.Model;

namespace FitApp.Api.Helper
{
    public static class MapHelper
    {
        public static User ToCreateUser(this CreateUserModel model, Guid customerId)
        {
            return new User
            {
                Id = customerId,
                CustomerName = model.CustomerName,
                CustomerSurname = model.CustomerSurname,
                CustomerMail = model.CustomerMail,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                Height = model.Height,
                Weight = model.Weight,
                WorkoutRate = model.WorkoutRate,
                UserStatus = model.UserStatus,
                WorkoutExperience = model.WorkoutExperience,
                Goal = model.Goal,
                BirthDate = model.BirthDate,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
            };
        }
        
        public static User ToCreateUser(this CreateUserWithSocialMediaModel model, Guid customerId)
        {
            return new User
            {
                Id = customerId,
                CustomerName = model.CustomerName,
                CustomerSurname = model.CustomerSurname,
                CustomerMail = model.CustomerMail,
                PhoneNumber = model.PhoneNumber,
                Height = model.Height,
                Weight = model.Weight,
                WorkoutRate = model.WorkoutRate,
                UserStatus = model.UserStatus,
                WorkoutExperience = model.WorkoutExperience,
                Goal = model.Goal,
                BirthDate = model.BirthDate,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
            };
        }
        
        public static User ToUpdateUser(this UpdateUserModel model, User user)
        {
            return new User
            {
                Id = user.Id,
                CustomerName = model.CustomerName ?? user.CustomerName,
                CustomerSurname = model.CustomerSurname ?? user.CustomerSurname,
                CustomerMail = model.CustomerMail ?? user.CustomerMail,
                Password = model.Password ?? user.Password,
                Height = model.Height ?? user.Height,
                Weight = model.Weight ?? user.Weight,
                WorkoutRate = model.WorkoutRate ?? user.WorkoutRate,
                UserStatus = model.UserStatus ?? user.UserStatus,
                WorkoutExperience = model.WorkoutExperience ?? user.WorkoutExperience,
                Goal = model.Goal ?? user.Goal,
                BirthDate = model.BirthDate ?? user.BirthDate,
                IsDeleted = model.IsDeleted ?? user.IsDeleted,
            };
        }
        
        public static TrainingRepository.Model.Training ToCreateTraining(this CreateTrainingModel model)
        {
            return new TrainingRepository.Model.Training
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                IsPopular = model.IsPopular,
                TrainingDifficulty = model.TrainingDifficulty,
                TrainingDuration = model.TrainingDuration,
                CreatedAt = DateTime.Now,
                SetIds = model.SetIds,
            };
        }
        
        public static Set ToCreateSet(this CreateSetModel model)
        {
            return new Set
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                ActivityId = model.ActivityId,
                ActivityNumber = model.ActivityNumber,
                ActivityRepetition = model.ActivityRepetition,
                CreatedAt = DateTime.Now,
            };
        }
        
        public static Set ToUpdateSet(this UpdateSetModel model, Set set)
        {
            return new Set
            {
                Id = set.Id,
                Name = model.Name ?? set.Name,
                ActivityId = model.ActivityId ?? set.ActivityId,
                ActivityRepetition = model.ActivityRepetition ?? set.ActivityRepetition,
                ActivityNumber = model.ActivityNumber ?? set.ActivityNumber
            };
        }
        
        public static Activity ToCreateActivity(this CreateActivityModel model)
        {
            return new Activity
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Difficulty = model.ActivityDifficulty,
                Equipment = model.ActivityEquipment,
                Status = model.ActivityStatus,
                EffectiveZone = model.ActivityEffectiveZone,
                EffectiveZonePrimary = model.ActivityEffectiveZonePrimary,
                EffectiveZoneSecondary = model.ActivityEffectiveZoneSecondary,
                CreatedAt = DateTime.Now,
            };
        }
        
        public static Nutrition ToCreateNutrition(this CreateNutritionModel model)
        {
            return new Nutrition
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                //Amount = model.Amount,
                Calorie = model.Calorie,
                Protein = model.Protein,
                Unit = model.Unit,
                CreatedAt = DateTime.Now,
            };
        }
        
        public static Nutrition ToUpdateNutrition(this UpdateNutritionModel model, Nutrition nutrition)
        {
            return new Nutrition
            {
                Id = nutrition.Id,
                Calorie = model.Calorie?? nutrition.Calorie,
                Name = model.Name?? nutrition.Name,
                Protein = model.Protein?? nutrition.Protein,
                Unit = model.Unit?? nutrition.Unit,
            };
        }
        
        public static MealNutrition ToCreateMealNutrition(this CreateMealNutritionModel model, Nutrition nutrition)
        {
            return new MealNutrition
            {
                Id = Guid.NewGuid(),
                Factor = model.Factor,
                NutritionId = model.NutritionId,
                TotalCalories = nutrition.Calorie * model.Factor,
                TotalProtein = nutrition.Protein * model.Factor,
                CreatedAt = DateTime.Now,
            };
        }
        
        public static Meal ToCreateMeal(this CreateMealModel model)
        {
            return new Meal
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Type = model.Type,
                IsVisible = model.IsVisible,
                MealNutritionIds = model.MealNutritionIds,
                CreatedAt = DateTime.Now,
            };
        }
        
        public static UserPrivateTraining ToUserPrivateTraining(this CreateUserPrivateTrainingModel model, Guid userId)
        {
            return new UserPrivateTraining
            {
                Id = userId,
                Disease = model.Disease,
                Equipment = model.Equipment,
                Goal = model.Goal,
                Level = model.Level,
                AdditionalInformation = model.AdditionalInformation,
                PrimaryZone = model.PrimaryZone,
                TrainingLocation = model.TrainingLocation,
                TrainingDayCountOfWeek = model.TrainingDayCountOfWeek,
                CreatedAt = DateTime.Now
            };
        }
        
        public static UserPrivateDiet ToUserPrivateDiet(this CreateUserPrivateDietModel model, Guid userId)
        {
            return new UserPrivateDiet
            {
                Id = userId,
                Goal = model.Goal,
                DislikedFoods = model.DislikedFoods,
                LikedFoods = model.LikedFoods,
                AdditionalInformation = model.AdditionalInformation,
                CreatedAt = DateTime.Now,
                Disease = model.Disease
            };
        }
    }
}