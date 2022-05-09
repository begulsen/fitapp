using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FitApp.Api.Controllers.MealController
{
    public class CreateMealModel : IValidatableObject
    {
        public string Name { get; set; }
        public List<Guid> MealNutritionIds { get; set; }
        [Required]
        [RegularExpression("breakfast|snack|lunch|dinner", ErrorMessage = "Invalid Meal Type")]
        public string Type { get; set; }
        public bool IsVisible { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult("MealName is null or empty!");
            if (MealNutritionIds == null || !MealNutritionIds.Any())
                yield return new ValidationResult("MealNutritionId is null or empty!");
        }
    }
}