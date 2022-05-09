namespace FitApp.TrainingRepository.Setttings
{
    public class GenericRepositorySettings
    {
        public virtual string IndexName { get; set; }
        public int? NumberOfShards { get; set; } = 2;
        public int? NumberOfReplicas { get; set; } = 1;
    }
}