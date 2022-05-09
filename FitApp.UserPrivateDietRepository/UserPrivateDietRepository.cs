using FitApp.UserPrivateDietRepository.Abstract;
using FitApp.UserPrivateDietRepository.Model;
using FitApp.UserPrivateDietRepository.Settings;
using Nest;

namespace FitApp.UserPrivateDietRepository
{
    public class UserPrivateDietRepository : GenericRepository<UserPrivateDiet>, IUserPrivateDietRepository
    {
        public UserPrivateDietRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient,
            settings)
        {
        }
    }
}