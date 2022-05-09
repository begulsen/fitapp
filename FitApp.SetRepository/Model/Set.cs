using System;

namespace FitApp.SetRepository.Model
{
    public class Set : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid ActivityId { get; set; } 
        public int ActivityNumber { get; set; }
        public int ActivityRepetition { get; set; }
    }
}