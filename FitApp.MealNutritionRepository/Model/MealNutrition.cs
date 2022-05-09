using System;

namespace FitApp.MealNutritionRepository.Model
{
    public class MealNutrition : Entity<Guid>
    {
        public Guid NutritionId { get; set; }
        public int Factor { get; set; }
        public int TotalCalories { get; set; }
        public int TotalProtein { get; set; }
    }
}
