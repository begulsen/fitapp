using System;
using System.Threading.Tasks;
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
        
        public Task DeleteAsync(Guid userId)
        {
            if (userId == default) throw new ArgumentNullException(nameof(userId));
            var result = SessionClient.DeleteAsync<UserPrivateTrainingDetail>(userId).GetAwaiter().GetResult();
            HandleResult(result);
            return Task.CompletedTask;
        }
    }
}