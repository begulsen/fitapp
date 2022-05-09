using System.Collections.Generic;

namespace FitApp.Api.Controllers.MenuController.Model
{
    public class ResponseMenuModel
    {
        public List<Day> Days{ get; set; }
    }

    public class Day
    {
        public List<MenuMealResponse> MealList { get; set; }
        public int DailyTotalCalories { get; set; }
        public int DailyTotalProtein { get; set; }
    }

    public class MenuMealResponse
    {
        public string MealName { get; set; }
        public string NutritionName { get; set; }
        public int NutritionTotalCalories { get; set; }
        public int NutritionTotalProtein { get; set; }
        public string MealType { get; set; }
        public string NutritionUnit { get; set; }
        public bool IsVisible { get; set; }
    }
}