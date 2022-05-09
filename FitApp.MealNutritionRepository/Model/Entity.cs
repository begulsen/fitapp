using System;

namespace FitApp.MealNutritionRepository.Model
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