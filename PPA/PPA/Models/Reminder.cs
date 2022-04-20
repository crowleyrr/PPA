using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPA.Models
{
    public class Reminder
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string ReminderName { get; set; }
        public DateTime ReminderTime { get; set; }
    }
}
