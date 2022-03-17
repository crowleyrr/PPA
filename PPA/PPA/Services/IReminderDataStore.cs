using PPA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPA.Services
{
    public interface IReminderDataStore
    {

        Task AddReminderAsync(Reminder item);
/*        Task UpdateReminderAsync(Reminder item);*/
        Task DeleteReminderAsync(int id);
        Task<Reminder> GetReminderAsync(int reminderId);
        Task<IEnumerable<Reminder>> GetRemindersAsync();
    }
}