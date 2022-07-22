using System;

namespace FitApp.Api.Controllers.MealNutritionController.Model
{
    public class ResponseMealNutritionBackoffice
    {
        public Guid NutritionId { get; set; }
        public String NutritionName { get; set; }
        public String Unit { get; set; }
        public int Factor { get; set; }
        public int TotalCalories { get; set; }
        public int TotalProtein { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}