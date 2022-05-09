using System;

namespace FitApp.UserPrivateTrainingRepository.Model
{
    public abstract class Entity<T>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}