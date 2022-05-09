using FitApp.ActivityRepository.Settings;

namespace FitApp.MenuRepository.Settings
{
    public class MenuRepositorySettings : GenericRepositorySettings
    { 
        public override string IndexName { get; set; } = "menu-index";
    }
}