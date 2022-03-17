using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PPA.Services
{
   public interface IDataStore<T>
    {
        /* Tasks */
        Task<bool> AddTaskAsync(T item);
        Task<bool> UpdateTaskAsync(T item);
        Task<bool> DeleteTaskAsync(string id);
        Task<T> GetTaskAsync(string id);
        Task<IEnumerable<T>> GetTasksAsync(bool forceRefresh = false);

        /* Reminders */

        Task<bool> AddReminderAsync(T item);
        Task<bool> UpdateReminderAsync(T item);
        Task<bool> DeleteReminderAsync(string id);
        Task<T> GetReminderAsync(string reminderId);
        Task<IEnumerable<T>> GetRemindersAsync(bool forceRefresh = false);

        /* Events */

        Task<bool> AddEventAsync(T item);
        Task<bool> UpdateEventAsync(T item);
        Task<bool> DeleteEventAsync(string id);
        Task<bool> GetEventAsync(string id);
        Task<IEnumerable<T>> GetEventsAsync(bool forceRefresh = false);
    }
}
