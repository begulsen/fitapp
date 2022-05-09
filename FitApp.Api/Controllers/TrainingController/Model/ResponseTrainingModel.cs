using System.Collections.Generic;

namespace FitApp.Api.Controllers.TrainingController.Model
{
    public class ResponseTrainingModel
    {
        public string Name { get; set; }
        public List<SetResponse> Sets { get; set; }
        public bool IsPopular{ get; set; }
        public string TrainingDuration { get; set; }
        public string TrainingDifficulty { get; set; }
    }

    public class SetResponse
    {
        public string SetName { get; set; }
        public int ActivityNumber { get; set; }
        public int ActivityRepetition { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDifficulty { get; set; }
        public string ActivityStatus { get; set; }
        public string ActivityEffectiveZone { get; set; }
        public string ActivityEffectiveZonePrimary { get; set; }
        public string ActivityEffectiveZoneSecondary { get; set; }
        public string ActivityEquipment { get; set; }
    }
    
   
}