namespace FitApp.UserRepository.Settings
{
    public class UserRepositorySettings : GenericRepositorySettings
    {
        public override string IndexName { get; set; } = "user-index";
    }
}