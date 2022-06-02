using System;
using System.Threading.Tasks;
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

        public Task DeleteAsync(Guid userId)
        {
            if (userId == default) throw new ArgumentNullException(nameof(userId));
            var result = SessionClient.DeleteAsync<UserPrivateTraining>(userId).GetAwaiter().GetResult();
            HandleResult(result);
            return Task.CompletedTask;
        }
    }
}