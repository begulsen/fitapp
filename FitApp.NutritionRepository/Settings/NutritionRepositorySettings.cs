namespace FitApp.NutritionRepository.Settings
{
    public class NutritionRepositorySettings : GenericRepositorySettings
    { 
        public override string IndexName { get; set; } = "nutrition-index";
    }
}