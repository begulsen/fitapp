namespace FitApp.ActivityRepository.Settings
{
    public class ActivityRepositorySettings : GenericRepositorySettings
    { 
        public override string IndexName { get; set; } = "activity-index";
    }
}