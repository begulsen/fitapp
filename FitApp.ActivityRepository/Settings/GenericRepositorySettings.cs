namespace FitApp.ActivityRepository.Settings
{
    public class GenericRepositorySettings
    {
        public virtual string IndexName { get; set; }
        public int? NumberOfShards { get; set; } = 1;
        public int? NumberOfReplicas { get; set; } = 1;
    }
}