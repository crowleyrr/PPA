using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PPA.Models
{
    public class Event
    {
        /*
         * An Event represents a calendar event.  The Event object contains the following attributes:
         * 
         * ID: unique identifier for specific Event
         * Title: The title of the Event
         * Description: Any pertinent details of the Event (ex: Location, things to bring, etc.)
         * DateTime: The specific time the Event is to occur
         */

        [PrimaryKey]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Today;
    }
}
