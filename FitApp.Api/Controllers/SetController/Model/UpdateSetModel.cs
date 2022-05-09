using System;

namespace FitApp.Api.Controllers.SetController.Model
{
    public class UpdateSetModel
    {
        public string Name { get; set; }
        public Guid? ActivityId { get; set; } 
        public int? ActivityNumber { get; set; }
        public int? ActivityRepetition { get; set; }
    }
}