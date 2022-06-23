using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitApp.UserPrivateTrainingRepository.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task SaveAsync(TEntity entity);
        Task<TEntity> GetAsync(string id);
        Task<bool> DeleteAsync(string id);
        Task BulkSaveAsync(IEnumerable<TEntity> entityList);
        Task<(List<TEntity>, string)> ScrollAsync(string scrollId, string scrollTimeout = "2m");
    }
}