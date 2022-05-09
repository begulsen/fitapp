using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitApp.Api.Controllers.SetController.Model
{
    public class CreateSetModel : IValidatableObject
    {
        public string Name { get; set; }
        public Guid ActivityId { get; set; } 
        public int ActivityNumber { get; set; }
        public int ActivityRepetition { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
                yield return new ValidationResult("Setname cannot be null or empty !");
            if (ActivityId == default)
                yield return new ValidationResult("ActivityId cannot be zero or negative value!");
            
            if (ActivityNumber == 0 || ActivityNumber < 0 )
                yield return new ValidationResult("Number of Activity cannot be zero or negative value!");
            
            if (ActivityRepetition == 0 || ActivityRepetition < 0 )
                yield return new ValidationResult("Repetition of Activity cannot be zero or negative value!");
        }
    }
}