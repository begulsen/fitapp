using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.MenuRepository.Model;

namespace FitApp.MenuRepository.Abstract
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        Task<List<Menu>> GetAll();
        Task<Menu> GetMenuByNameAsync(string menuName);
    }
}