using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.TrainingRepository.Abstract;
using FitApp.TrainingRepository.Model;
using FitApp.TrainingRepository.Setttings;
using Nest;

namespace FitApp.TrainingRepository
{
    public class TrainingRepository : GenericRepository<Training, Guid>, ITrainingRepository
    {
        public TrainingRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient,
            settings)
        {
        }
    
        public async Task<Training> GetTraining(string trainingId)
        {

            var searchDescriptor = new SearchDescriptor<Training>()
                .Index(IndexName)
                .Query(q => q.Term(f => f.Field(trainingId).Value(trainingId)));

            var result = await SessionClient.SearchAsync<Training>(searchDescriptor);
            if (result.Documents != null && result.Documents.Any())
            {
                return result.Documents.FirstOrDefault();
            }
            return null;
        }

        public async Task<List<Training>> GetAll()
        {
            var trainingList = new List<Training>();

            var searchDescriptor = new SearchDescriptor<Training>()
                .Index(IndexName)
                .Take(1000)
                .Query(q => q.MatchAll())
                .Scroll("2m");

            var result = await SessionClient.SearchAsync<Training>(searchDescriptor);
            if (result.Documents != null && result.Documents.Any())
            {
                trainingList.AddRange(result.Documents);
            }

            var scrollId = result.ScrollId;
            while (!string.IsNullOrEmpty(scrollId))
            {
                List<Training> trainings;
                (trainings, scrollId) = await ScrollAsync(scrollId);
                if (trainings != null && trainings.Any())
                {
                    trainingList.AddRange(trainings);
                }
                else
                    break;
            }

            return trainingList;
        }
    
        public Task Update(Guid id, List<Guid> trainingIds)
        {
            if (trainingIds == null) throw new ArgumentNullException(nameof(trainingIds));
            var result = SessionClient.Update<Training, object>(id, descriptor => descriptor
                .Doc(new
                {
                    trainingIds
                })
                .Index(IndexName));
            
            HandleResult(result);

            return Task.CompletedTask;
        }

        public Task<Training> GetTrainingByNameAsync(string trainingName)
        {
            if (string.IsNullOrEmpty(trainingName)) throw new ArgumentNullException(nameof(trainingName));
            var result = SessionClient.SearchAsync<Training>(s => s
                .Take(1)
                .Query(x => x
                    .Term(m => m
                        .Field(f => f.Name.Suffix("keyword"))
                        .Value(trainingName)))
                .Index(IndexName)).GetAwaiter().GetResult();

            HandleResult(result);
            return Task.FromResult(result.Documents.FirstOrDefault());
        }
    }
}