using System;

namespace FitApp.UserPrivateDietRepository.Model
{
    public abstract class Entity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}