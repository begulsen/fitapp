using System;
using System.Collections.Generic;

namespace FitApp.MealRepository.Model
{
    public class Meal : Entity<Guid>
    {
        public string Name { get; set; }
        public List<Guid> MealNutritionIds { get; set; }
        public string Type { get; set; }
        public bool IsVisible { get; set; }
    }
}