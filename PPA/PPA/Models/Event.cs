using System;
using System.Collections.Generic;
using System.Text;

namespace PPA.Models
{
    internal class Event
    {
        public string Id { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }

    }
}
