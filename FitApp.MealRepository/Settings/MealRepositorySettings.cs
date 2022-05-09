namespace FitApp.MealRepository.Settings
{
    public class MealRepositorySettings : GenericRepositorySettings
    { 
        public override string IndexName { get; set; } = "meal-index";
    }
}