using System;
using System.Threading.Tasks;
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
        
        public Task DeleteAsync(Guid userId)
        {
            if (userId == default) throw new ArgumentNullException(nameof(userId));
            var result = SessionClient.DeleteAsync<UserPrivateDietDetail>(userId).GetAwaiter().GetResult();
            HandleResult(result);
            return Task.CompletedTask;
        }
    }
}