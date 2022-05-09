using System;

namespace FitApp.TrainingRepository.Model
{
    public abstract class Entity<T> : EntityBase<T>
    {
        public DateTime CreatedAt { get; set; }
    }

    public abstract class EntityBase<T>
    {
        public T Id { get; set; }
    }
}