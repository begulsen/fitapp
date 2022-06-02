using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.UserRepository.Abstract;
using FitApp.UserRepository.Model;
using FitApp.UserRepository.Settings;
using Nest;

namespace FitApp.UserRepository
{
    public class UserRepository: GenericRepository<User, Guid>, IUserRepository
    {
        public UserRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient, settings) { }
        
        public async Task<List<User>> GetAll()
        {
            var merchantList = new List<User>();

             var searchDescriptor = new SearchDescriptor<User>()
                 .Index(IndexName)
                 .Take(1000)
                 .Query(q => q.MatchAll())
                 .Scroll("2m");
             
             var result = await SessionClient.SearchAsync<User>(searchDescriptor);
             if (result.Documents != null && result.Documents.Any())
             {
                 merchantList.AddRange(result.Documents);
             }
             var scrollId = result.ScrollId;
             while (!string.IsNullOrEmpty(scrollId))
             {
                 List<User> users;
                 (users, scrollId) = await ScrollAsync(scrollId);
                 if (users != null && users.Any())
                 {
                     merchantList.AddRange(users);
                 }
                 else
                     break;
             }
             return merchantList;
        }

        public Task DeleteAsync(Guid userId)
        {
            if (userId == default) throw new ArgumentNullException(nameof(userId));
            var result = SessionClient.DeleteAsync<User>(userId).GetAwaiter().GetResult();
            HandleResult(result);
            return Task.CompletedTask;
        }
        
        public Task Update(UserPartial updateModel)
        {
            if (updateModel == null) throw new ArgumentNullException(nameof(updateModel));
            var result = SessionClient.Update<User, object>(updateModel.CustomerId, descriptor => descriptor
                .Doc(new
                {
                    updateModel.Height,
                    updateModel.Weight,
                    updateModel.Password,
                    updateModel.CustomerMail,
                    updateModel.CustomerName,
                    updateModel.CustomerSurname,
                    updateModel.WorkoutExperience,
                    updateModel.WorkoutRate,
                    updateModel.Goal,
                    updateModel.UserStatus
                })
                .Index(IndexName));
            
            HandleResult(result);

            return Task.CompletedTask;
        }

        public Task<User> GetUserByMail(string customerMail)
        {
            if (default == customerMail) throw new ArgumentNullException(nameof(customerMail));

            var searchDescriptor = new SearchDescriptor<User>().Index(IndexName).Take(1);

            searchDescriptor.Query(x => x
                .Term(m => m
                    .Field(f => f.CustomerMail.Suffix("keyword"))
                    .Value(customerMail)));

            var result = SessionClient.SearchAsync<User>(searchDescriptor).GetAwaiter().GetResult();
            HandleResult(result);
            return Task.FromResult(result.Documents.FirstOrDefault());        }
    }
}