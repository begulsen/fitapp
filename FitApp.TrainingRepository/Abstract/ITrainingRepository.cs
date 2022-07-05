using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.TrainingRepository.Model;

namespace FitApp.TrainingRepository.Abstract
{
    public interface ITrainingRepository : IGenericRepository<Training>
    {
        Task<Training> GetTraining(string trainingId);
        Task<List<Training>> GetAll();
        Task Update(Guid id, List<Guid> activityIds);
        Task<List<Training>> GetTrainingByNamesAsync(List<string> trainingNames);
    }
}