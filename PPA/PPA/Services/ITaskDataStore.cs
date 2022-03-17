using PPA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPA.Services
{
    public interface ITaskDataStore
    {

        Task AddTaskAsync(TaskItem item);
        /*Task UpdateTaskAsync(TaskItem item);*/
        Task DeleteTaskAsync(int id);
        Task<TaskItem> GetTaskAsync(int id);
        Task<IEnumerable<TaskItem>> GetTasksAsync();
    }
}