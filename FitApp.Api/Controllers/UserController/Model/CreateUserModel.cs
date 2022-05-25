using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitApp.Api.Controllers.UserController.Model
{
    public class CreateUserModel : IValidatableObject
    {
        public string CustomerMail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }
        public decimal Height { get; set; }
        public bool CreateWithSocial { get; set; } = false;
        public decimal Weight { get; set; }
        [Required]
        [RegularExpression("none|twoTimesWeek|fourTimesWeek|moreThanFourTimesWeek", ErrorMessage = "Invalid WorkoutRate")]
        public string WorkoutRate { get; set; }
        [Required]
        [RegularExpression("basic|intermediate|premium", ErrorMessage = "Invalid UserStatus")]
        public string UserStatus { get; set; }
        [Required]
        [RegularExpression("starter|average|pro", ErrorMessage = "Invalid WorkoutExperience")]
        public string WorkoutExperience { get; set; }
        [Required]
        [RegularExpression("power|fit|muscle|weightLoss", ErrorMessage = "Invalid Goal")]
        public string Goal { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(CustomerName))
                yield return new ValidationResult("CustomerName is null or empty!");
            if (string.IsNullOrEmpty(CustomerSurname))
                yield return new ValidationResult("CustomerSurname is null or empty!");
            if (string.IsNullOrEmpty(CustomerMail))
                yield return new ValidationResult("CustomerMail is null or empty!");
            if (string.IsNullOrEmpty(Password)) 
                yield return new ValidationResult("Password is null or empty!");
        }
    }
}