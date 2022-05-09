using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitApp.ActivityRepository.Abstract;
using FitApp.ActivityRepository.Model;
using FitApp.ActivityRepository.Settings;
using Nest;

namespace FitApp.ActivityRepository
{
    public class ActivityRepository : GenericRepository<Activity, Guid>, IActivityRepository
    {
        public ActivityRepository(ElasticClient elasticClient, GenericRepositorySettings settings) : base(elasticClient,
            settings)
        {
        }

        public async Task<List<Activity>> GetAll(List<string>? equipmentList, string effectiveZone)
        {
            var setList = new List<Activity>();

            QueryContainer GenerateQuery(QueryContainerDescriptor<Activity> q)
            {
                var query = new QueryContainer();
                query = query && q.MatchAll();
                
                if (equipmentList != null && equipmentList.Any())
                {
                    query = query && q.Terms(t => t.Field(f => f.Equipment).Terms(equipmentList));
                }

                if (!string.IsNullOrEmpty(effectiveZone))
                {
                    query = query && q.Term(t => t.Field(f => f.EffectiveZone).Value(effectiveZone));
                }
                
                return query;
            }
            var searchDescriptor = new SearchDescriptor<Activity>()
                .Index(IndexName)
                .Take(1000)
                .Query(GenerateQuery)
                .Scroll("2m");

            var result = await SessionClient.SearchAsync<Activity>(searchDescriptor);
            if (result.Documents != null && result.Documents.Any())
            {
                setList.AddRange(result.Documents);
            }

            var scrollId = result.ScrollId;
            while (!string.IsNullOrEmpty(scrollId))
            {
                List<Activity> activities;
                (activities, scrollId) = await ScrollAsync(scrollId);
                if (activities != null && activities.Any())
                {
                    setList.AddRange(activities);
                }
                else
                    break;
            }

            return setList;
        }

        public Task<Activity> GetActivityByNameAsync(string activityName)
        {
            if (string.IsNullOrEmpty(activityName)) throw new ArgumentNullException(nameof(activityName));
            var result = SessionClient.SearchAsync<Activity>(s => s
                .Take(1)
                .Query(q => q.Term(m => m
                    .Field(f => f.Name.Suffix("keyword"))
                    .Value(activityName)))
                .Index(IndexName)).GetAwaiter().GetResult();

            HandleResult(result);
            return Task.FromResult(result.Documents.FirstOrDefault());
        }
    }
}