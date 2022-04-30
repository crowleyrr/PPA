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


        /*
         * Creates the database to hold Reminder objects.
         *
         * If database already exists, then nothing is done
         */
        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "ReminderData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<PPA.Models.Reminder>();
        }


        /*
         * Adds a new Reminder object to the database
         */
        public async Task AddReminderAsync(PPA.Models.Reminder reminder)
        {
            await Init();
            reminder.Id = Guid.NewGuid();
            var id = await db.InsertAsync(reminder);
        }

        /*
         * Deletes an existing Reminder object from the database
         */
        public async Task DeleteReminderAsync(Guid id)
        {
            await Init();
            await db.DeleteAsync<PPA.Models.Reminder>(id);
        }

        /*
         * Returns all Reminder objects as a list
         */
        public async Task<IEnumerable<PPA.Models.Reminder>> GetRemindersAsync()
        {
            await Init();

            var reminders = await db.Table<PPA.Models.Reminder>().ToListAsync();
            return reminders;
        }

    }
}