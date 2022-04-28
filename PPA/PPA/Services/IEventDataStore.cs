using PPA.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPA.Services
{
    public interface IEventDataStore
    {
        Task AddEventAsync(Event item);
        Task DeleteEventAsync(Guid id);
       // Task<Event> GetEventAsync(int id);
        Task<IEnumerable<Event>> GetEventsAsync();
    }
}