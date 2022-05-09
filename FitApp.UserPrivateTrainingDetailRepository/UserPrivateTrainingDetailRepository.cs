using FitApp.UserPrivateTrainingDetailRepository.Abstract;
using FitApp.UserPrivateTrainingDetailRepository.Model;
using FitApp.UserPrivateTrainingDetailRepository.Settings;
using Nest;

namespace FitApp.UserPrivateTrainingDetailRepository
{
    public class UserPrivateTrainingDetailRepository : GenericRepository<UserPrivateTrainingDetail>, IUserPrivateTrainingDetailRepository
    {
        public UserPrivateTrainingDetailRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient,
            settings)
        {
        }
    }
}