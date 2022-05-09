using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.UserRepository.Model;

namespace FitApp.UserRepository.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetAll();
        Task Update(UserPartial updateModel);
    }
}