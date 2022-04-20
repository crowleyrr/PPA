using PPA.Services;
using PPA.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReminderService))]

namespace PPA.Services
{
    public class ReminderService : IReminderDataStore
    {
        SQLiteAsyncConnection db;

        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "ReminderData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<PPA.Models.Reminder>();
        }

        public async Task AddReminderAsync(PPA.Models.Reminder reminder)
        {
            await Init();
            reminder.Id = Guid.NewGuid();
            var id = await db.InsertAsync(reminder);
        }

        public async Task DeleteReminderAsync(Guid id)
        {
            await Init();
            await db.DeleteAsync<PPA.Models.Reminder>(id);
        }

        public async Task<IEnumerable<PPA.Models.Reminder>> GetRemindersAsync()
        {
            await Init();

            var reminders = await db.Table<PPA.Models.Reminder>().ToListAsync();
            return reminders;
        }

        public async Task<PPA.Models.Reminder> GetReminderAsync(Guid id)
        {
            await Init();
            var reminder = await db.Table<PPA.Models.Reminder>().FirstOrDefaultAsync(c => c.Id == id);

            return reminder;
        }
    }
}

/*


using PPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reminder = PPA.Models.Reminder;

namespace PPA.Services
{
    public class ReminderService
    {
        readonly List<Reminder> reminders;

        public ReminderService()
        {
            reminders = new List<Reminder>()
            {
                new Reminder { Id = Guid.NewGuid().ToString(), ReminderName = "First item", ReminderTime=new DateTime(2022, 3, 20, 11, 30, 00) },
                new Reminder { Id = Guid.NewGuid().ToString(), ReminderName = "Second item", ReminderTime=new DateTime(2022, 3, 22, 9, 00, 00) },
            };
        }

        public async Task<bool> AddReminderAsync(Reminder reminder)
        {
            reminders.Add(reminder);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateReminderAsync(Reminder reminder)
        {
            var oldReminder = reminders.Where((Reminder arg) => arg.Id == reminder.Id).FirstOrDefault();
            reminders.Remove(oldReminder);
            reminders.Add(reminder);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteReminderAsync(string id)
        {
            var oldReminder = reminders.Where((Reminder arg) => arg.Id == id).FirstOrDefault();
            reminders.Remove(oldReminder);

            return await Task.FromResult(true);
        }

        public async Task<Reminder> GetReminderAsync(string id)
        {
            return await Task.FromResult(reminders.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Reminder>> GetRemindersAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(reminders);
        }
    }
}
*/