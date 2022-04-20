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

        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "EventData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Event>();
        }

        public async Task AddEventAsync(Event ev)
        {
            await Init();
            var id = await db.InsertAsync(ev);
        }

         
        public async Task DeleteEventAsync(int id)
        {
            await Init();
            await db.DeleteAsync<Event>(id);
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            await Init();

            var events = await db.Table<Event>().ToListAsync();
            return events;
        }

        /*
        public async Task<Event> GetEventAsync(int id)
        {
            await Init();
            var ev = await db.Table<Event>().FirstOrDefaultAsync(c => c.Id == id);

            return ev;
        } */
        
    }
}

/*

using PPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event = PPA.Models.Event;

namespace PPA.Services
{
    public class EventService
    {
        readonly List<Event> events;

        public EventService()
        {
            events = new List<Event>()
            {
                new Event { Id = Guid.NewGuid().ToString(), EventName = "First item", EventLocation="Location 1.", EventStartTime = new DateTime(2022,3,20, 11, 30, 00), EventEndTime = new DateTime(2022, 3, 20, 12, 00, 00) },
                new Event { Id = Guid.NewGuid().ToString(), EventName = "Second item", EventLocation="Location 2.", EventStartTime = new DateTime(2022, 3, 22, 14, 00, 00), EventEndTime = new DateTime(2022, 3, 22, 16, 00, 00) },
            };
        }

        public async Task<bool> AddEventAsync(Event ev)
        {
            events.Add(ev);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateEventAsync(Event ev)
        {
            var oldEvent = events.Where((Event arg) => arg.Id == ev.Id).FirstOrDefault();
            events.Remove(oldEvent);
            events.Add(ev);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteEvetntAsync(string id)
        {
            var oldEvent = events.Where((Event arg) => arg.Id == id).FirstOrDefault();
            events.Remove(oldEvent);

            return await Task.FromResult(true);
        }

        public async Task<Event> GetEventAsync(string id)
        {
            return await Task.FromResult(events.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(events);
        }
    }
}
*/