using System;
using System.Collections.Generic;

namespace FitApp.Api.Controllers.TrainingController.Model
{
    public class CreateTrainingModel
    {
        public string Name { get; set; }
        public List<Guid> SetIds { get; set; }
        public bool IsPopular{ get; set; }
        public string TrainingDuration { get; set; }
        public string TrainingDifficulty { get; set; }
    }
}