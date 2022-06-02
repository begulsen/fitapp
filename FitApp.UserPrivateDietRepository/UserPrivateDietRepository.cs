using System;
using System.Threading.Tasks;
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

        public Task DeleteAsync(Guid userId)
        {
            if (userId == default) throw new ArgumentNullException(nameof(userId));
            var result = SessionClient.DeleteAsync<UserPrivateDiet>(userId).GetAwaiter().GetResult();
            HandleResult(result);
            return Task.CompletedTask;
        }
    }
}