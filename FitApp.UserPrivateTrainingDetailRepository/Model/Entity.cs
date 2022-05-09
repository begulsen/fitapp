using System;

namespace FitApp.UserPrivateTrainingDetailRepository.Model
{
    public abstract class Entity<T>
    {
        public DateTime CreatedAt { get; set; }
    }
}