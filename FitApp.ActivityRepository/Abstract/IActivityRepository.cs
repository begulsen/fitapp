using System.Collections.Generic;
using System.Threading.Tasks;
using FitApp.ActivityRepository.Model;

namespace FitApp.ActivityRepository.Abstract
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        Task<List<Activity>> GetAll(List<string>? equipmentList = null, string effectiveZone = null);
        Task<Activity> GetActivityByNameAsync(string menuName);
    }
}