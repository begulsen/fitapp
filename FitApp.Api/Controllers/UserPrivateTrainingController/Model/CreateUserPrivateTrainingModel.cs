using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FitApp.Api.Controllers.UserPrivateTrainingController.Model
{
    public class CreateUserPrivateTrainingModel : IValidatableObject
    {
        public IFormFile Image1 { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
        [Required]
        public List<string> Goal { get; set; }
        [Required]
        public List<string> PrimaryZone { get; set; }
        [Required]
        public string TrainingLocation { get; set; }
        [Required]
        public int TrainingDayCountOfWeek { get; set; }
        [Required]
        public string Equipment { get; set; }
        [Required]
        public string Level { get; set; }
        public string Disease { get; set; }
        public string AdditionalInformation { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Goal == null || !Goal.Any())
            {
                yield return new ValidationResult("Training goal is not valid! Goal cannot be null");            
            }
            if (PrimaryZone == null || !PrimaryZone.Any())
            {
                yield return new ValidationResult("Training primaryZone is not valid! PrimaryZone cannot be null");            
            }
        }
    }
}