using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPA.Models
{
    public class Reminder
    {
        /*
         * A Reminder represents something a user would like to be notified about.  A Reminder object contains the following attributes:
         * 
         * ID: Unique identifier for specific Reminder object
         * ReminderName: What the user would like to be reminded about
         * ReminderTime: The date and time the user would like to be notified at
         * 
         */

        [PrimaryKey]
        public Guid Id { get; set; }
        public string ReminderName { get; set; }
        public DateTime ReminderTime { get; set; }
    }
}
