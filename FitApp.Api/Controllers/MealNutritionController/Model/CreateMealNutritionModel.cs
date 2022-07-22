using System;
using Microsoft.VisualBasic;

namespace FitApp.Api.Controllers.MealNutritionController.Model
{
    public class CreateMealNutritionModel
    {
        public Guid NutritionId { get; set; }
        public int Factor { get; set; }
    }
}