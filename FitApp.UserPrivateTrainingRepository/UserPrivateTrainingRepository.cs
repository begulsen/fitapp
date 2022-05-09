using FitApp.UserPrivateTrainingRepository.Abstract;
using FitApp.UserPrivateTrainingRepository.Model;
using FitApp.UserPrivateTrainingRepository.Settings;
using Nest;

namespace FitApp.UserPrivateTrainingRepository
{
    public class UserPrivateTrainingRepository : GenericRepository<UserPrivateTraining>, IUserPrivateTrainingRepository
    {
        public UserPrivateTrainingRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient,
            settings)
        {
        }
    }
}