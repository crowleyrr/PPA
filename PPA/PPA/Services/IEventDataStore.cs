using PPA.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPA.Services
{
    /*
     * Interface for the Event Database
     */
    public interface IEventDataStore
    {
        Task AddEventAsync(Event item);
        Task DeleteEventAsync(Guid id);
        Task<IEnumerable<Event>> GetEventsAsync();
    }
}