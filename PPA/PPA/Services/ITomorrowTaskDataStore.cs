using PPA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PPA.Services
{
    public interface ITomorrowTaskDataStore
    {
        Task AddTaskAsync(TomorrowTaskItem item);
        /*Task UpdateTaskAsync(TaskItem item);*/
        Task DeleteTaskAsync(int id);
        Task<TomorrowTaskItem> GetTaskAsync(int id);
        Task<IEnumerable<TomorrowTaskItem>> GetTasksAsync();
    }
}
