using PPA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPA.Services
{
    public interface IEventDataStore
    {
        Task AddEventAsync(Event item);
        Task DeleteEventAsync(int id);
       // Task<Event> GetEventAsync(int id);
        Task<IEnumerable<Event>> GetEventsAsync();
    }
}