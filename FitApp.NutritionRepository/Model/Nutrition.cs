using System;

namespace FitApp.NutritionRepository.Model
{
    public class Nutrition : Entity<Guid>
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        //public int Amount { get; set; }
        public int Calorie { get; set; }
        public int Protein { get; set; }
    }
}