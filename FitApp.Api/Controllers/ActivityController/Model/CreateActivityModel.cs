using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitApp.Api.Controllers.ActivityController.Model
{
    public class CreateActivityModel : IValidatableObject
    {
        public string Name { get; set; }
        [RegularExpression("easy|medium|hard", ErrorMessage = "Invalid ActivityDifficulty")]
        public string ActivityDifficulty { get; set; }
        [RegularExpression("basic|premium", ErrorMessage = "Invalid ActivityStatus")]
        public string ActivityStatus { get; set; }
        public string ActivityEffectiveZone { get; set; }
        public string ActivityEffectiveZonePrimary { get; set; }
        public string ActivityEffectiveZoneSecondary { get; set; }
        public string ActivityEquipment { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult("ActivityName is null or empty!");
            if (string.IsNullOrEmpty(ActivityEquipment))
                yield return new ValidationResult("ActivityEquipment is null or empty!");
            if (string.IsNullOrEmpty(ActivityStatus))
                yield return new ValidationResult("ActivityStatus is null or empty!");
            if (string.IsNullOrEmpty(ActivityDifficulty))
                yield return new ValidationResult("ActivityDifficulty is null or empty!");
        }
    }
}