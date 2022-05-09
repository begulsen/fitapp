using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.SetRepository.Model;

namespace FitApp.SetRepository.Abstract
{
    public interface ISetRepository : IGenericRepository<Set>
    {
        Task<List<Set>> GetAll();
        Task<Set> GetSetByName(string setName);
    }
}