using System;
using System.Collections.Generic;
using System.Text;

namespace PPA.Models
{
    internal class Reminder
    {
        public string Id { get; set; }
        public string ReminderName { get; set; }
        public DateTime ReminderTime { get; set; }
    }
}
