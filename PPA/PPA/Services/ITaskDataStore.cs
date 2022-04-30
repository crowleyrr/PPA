using PPA.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPA.Services
{
    /*
     * Interface for the TaskItem database
     */
    public interface ITaskDataStore
    {

        Task AddTaskAsync(TaskItem item);
        Task DeleteTaskAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetTasksAsync();
    }
}