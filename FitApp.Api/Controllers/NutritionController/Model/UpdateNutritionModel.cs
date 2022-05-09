namespace FitApp.Api.Controllers.NutritionController.Model
{
    public class UpdateNutritionModel
    {
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public int? Amount { get; set; }
        public int? Calorie { get; set; }
        public int? Protein { get; set; }
    }
}