using System;
using System.Threading.Tasks;
using FitApp.UserPrivateTrainingDetailRepository.Model;
using FitApp.UserPrivateTrainingRepository.Abstract;

namespace FitApp.UserPrivateTrainingDetailRepository.Abstract
{
    public interface IUserPrivateTrainingDetailRepository : IGenericRepository<UserPrivateTrainingDetail>
    {
        Task DeleteAsync(Guid id);
    }
}