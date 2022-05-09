using System.Threading.Tasks;

namespace FitApp.Core
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<string> SaveAsync(TEntity entity);
    }
}