using PPA.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPA.Services
{

    /*
     * Interface for the Reminder Database
     */
    public interface IReminderDataStore
    {

        Task AddReminderAsync(Reminder item);
        Task DeleteReminderAsync(Guid id);
        Task<IEnumerable<Reminder>> GetRemindersAsync();
    }
}