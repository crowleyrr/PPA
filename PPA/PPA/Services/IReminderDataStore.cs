using PPA.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPA.Services
{
    public interface IReminderDataStore
    {

        Task AddReminderAsync(Reminder item);
/*        Task UpdateReminderAsync(Reminder item);*/
        Task DeleteReminderAsync(Guid id);
        Task<Reminder> GetReminderAsync(Guid id);
        Task<IEnumerable<Reminder>> GetRemindersAsync();
    }
}