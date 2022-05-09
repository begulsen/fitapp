namespace FitApp.UserPrivateTrainingRepository.Settings
{
    public class UserPrivateTrainingRepositorySettings : GenericRepositorySettings
    { 
        public override string IndexName { get; set; } = "user-private-training-index";
    }
}