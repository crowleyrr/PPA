using PPA.Services;
using PPA.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(EventService))]

namespace PPA.Services
{
    public class EventService : IEventDataStore
    {
        
        SQLiteAsyncConnection db;


        /*
         * Creates the database to hold Event objects.
         *
         * If database already exists, then nothing is done
         */
        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "EventData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Event>();
        }

        /*
         * Adds a new Event object to the database
         */
        public async Task AddEventAsync(Event ev)
        {
            await Init();
            ev.Id = Guid.NewGuid();
            var id = await db.InsertAsync(ev);
        }

         
        /*
         * Deletes an existing Event object based on the Id
         */
        public async Task DeleteEventAsync(Guid id)
        {
            await Init();
            await db.DeleteAsync<Event>(id);
        }

        /*
         * Returns all existing Event objects as a list
         */
        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            await Init();

            var events = await db.Table<Event>().ToListAsync();
            return events;
        }

        
    }
}