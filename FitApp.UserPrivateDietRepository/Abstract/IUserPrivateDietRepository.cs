using System;
using System.Threading.Tasks;
using FitApp.UserPrivateDietRepository.Model;

namespace FitApp.UserPrivateDietRepository.Abstract
{
    public interface IUserPrivateDietRepository : IGenericRepository<UserPrivateDiet>
    {
        Task DeleteAsync(Guid id);
    }
}