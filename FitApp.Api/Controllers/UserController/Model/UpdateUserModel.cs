using System;

namespace FitApp.Api.Controllers.UserController.Model
{
    public class UpdateUserModel
    {
        public string? CustomerMail { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Password { get; set; }
        public bool? IsDeleted { get; set; }
        public string? WorkoutRate { get; set; }
        public string? UserStatus { get; set; }
        public string? WorkoutExperience { get; set; }
        public string? Goal { get; set; }
    }
}