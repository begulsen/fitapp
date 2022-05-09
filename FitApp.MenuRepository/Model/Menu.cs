using System;
using System.Collections.Generic;

namespace FitApp.MenuRepository.Model
{
    public class Menu : Entity<Guid>
    {
        public string Name { get; set; }
        public List<Day> Days { get; set; }
    }

    public class Day
    {
        public List<Guid> MealIds { get; set; }
    }
}