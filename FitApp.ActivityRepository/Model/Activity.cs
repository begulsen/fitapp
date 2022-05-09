using System;

namespace FitApp.ActivityRepository.Model
{
    public class Activity : Entity<Guid>
    {
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public string Status { get; set; }
        public string EffectiveZone { get; set; }
        public string EffectiveZonePrimary { get; set; }
        public string EffectiveZoneSecondary { get; set; }
        public string Equipment { get; set; }
    }
}