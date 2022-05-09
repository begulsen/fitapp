using FitApp.TrainingRepository.Setttings;

namespace FitApp.TrainingRepository.Settings
{
    public class TrainingRepositorySettings : GenericRepositorySettings
    {
        public override string IndexName { get; set; } = "training-index";
    }

    class TrainingRepositorySettingsImpl : TrainingRepositorySettings
    {
    }
}