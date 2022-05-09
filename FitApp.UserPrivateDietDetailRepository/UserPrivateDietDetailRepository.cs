using FitApp.UserPrivateDietDetailRepository.Abstract;
using FitApp.UserPrivateDietDetailRepository.Model;
using FitApp.UserPrivateDietDetailRepository.Settings;
using Nest;

namespace FitApp.UserPrivateDietDetailRepository
{
    public class UserPrivateDietDetailRepository : GenericRepository<UserPrivateDietDetail>, IUserPrivateDietDetailRepository
    {
        public UserPrivateDietDetailRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient,
            settings)
        {
        }
    }
}