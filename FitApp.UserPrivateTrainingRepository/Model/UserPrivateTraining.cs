using System;
using System.Collections.Generic;

namespace FitApp.UserPrivateTrainingRepository.Model
{
    public class UserPrivateTraining : Entity<Guid>
    {
        public Guid Id { get; set; }
        public List<string> Goal { get; set; }
        public List<string> PrimaryZone { get; set; }
        public string TrainingLocation { get; set; }
        public int TrainingDayCountOfWeek { get; set; }
        public string Equipment { get; set; }
        public string Level { get; set; }
        public string Disease { get; set; }
        public string AdditionalInformation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}