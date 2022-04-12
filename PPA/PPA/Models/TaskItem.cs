using SQLite;
using System;

namespace PPA.Models
{
    public class TaskItem
    {
    
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean Today { get; set; }

    }
}
