using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FitApp.Api.Controllers.UserPrivateDietController.Model
{
    public class CreateUserPrivateDietModel : IValidatableObject
    {
        public IFormFile Image1 { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
        [Required]
        public List<string> Goal { get; set; }
        public List<string> LikedFoods { get; set; }
        public List<string> DislikedFoods { get; set; }
        public string Disease { get; set; }
        public string AdditionalInformation { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Goal == null || !Goal.Any())
            {
                yield return new ValidationResult("Diet goal is not valid! Goal cannot be null");            
            }
        }
    }
}