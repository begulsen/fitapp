using System;
using System.Linq;
using Elasticsearch.Net;
using FitApp.NutritionRepository.Abstract;
using FitApp.NutritionRepository.Settings;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace FitApp.NutritionRepository
{
    public static class NutritionRepositoryExtension
    {
        public static IServiceCollection AddNutritionRepository(this IServiceCollection services)
        {
            NutritionRepositorySettings settings = new NutritionRepositorySettings();
            /*switch (environmentMode)
        {
            case EnvironmentMode.Qa:
                // elasticUrl = "http://192.168.55.60:9200/";
                elasticUrl = "http://192.168.49.56:9200/";
                break;
            case EnvironmentMode.Prod:
                // elasticUrl = "http://10.168.100.102:9200/;http://10.168.100.103:9200/;http://10.168.100.104:9200/";
                
                elasticUrl = "http://10.168.36.81:9200/;http://10.168.36.82:9200/;http://10.168.36.83:9200/;" +
                             "http://10.168.36.84:9200/;http://10.168.36.85:9200/;http://10.168.36.86:9200/;" +
                             "http://10.168.36.87:9200/;http://10.168.36.88:9200/;http://10.168.36.89:9200/";
                
                settings.NumberOfReplicas = 1;
                settings.NumberOfShards = 5;
                break;
            // ReSharper disable once RedundantCaseLabel
            case EnvironmentMode.Local:
            // ReSharper disable once RedundantCaseLabel
            case EnvironmentMode.Test:
            default:
                // elasticUrl = "http://192.168.55.12:9200/";
                elasticUrl = "http://192.168.49.55:9200/";
                break;
        }*/

            //string elasticUrl = "http://localhost:9200/";
            var cloudId =
                "My_deployment:ZXUtY2VudHJhbC0xLmF3cy5jbG91ZC5lcy5pbzo0NDMkYzg0NTRhMjUwYjc5NDllZmFhYjMxNzU3MzA5ZWMyODckZmJlYWRiY2NkMzNmNDVjMTlhMTg1Yjg3YzI4MDhlMDg=";
            var credentials = new BasicAuthenticationCredentials("elastic", "eU369xKmWPI6RMdDZPXESler");
            var pool = new CloudConnectionPool(cloudId, credentials);
            var elasticConnectionSettings = new ConnectionSettings(pool).ThrowExceptions().EnableDebugMode();
            var elasticClient = new ElasticClient(elasticConnectionSettings);
            var repository = new NutritionRepository(elasticClient, settings);
            services.AddSingleton<INutritionRepository>(repository);

            return services;
        }
    }
}