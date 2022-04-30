using SQLite;
using System;

namespace PPA.Models
{
    public class TaskItem
    {

        /*
         * A TaskItem represents a task the user would like to accomplish but on no specific timeline.  
         * 
         * A TaskItem contains the following attributes:
         * ID: Unique identifier for the TaskItem object
         * Name: The name of the task
         * Description: Any pertinent details relating to the task
         * Today: A boolean denoting if the task is to be accomplished today, or has been pushed to tomorrow
         * 
         */
    
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean Today { get; set; }

    }
}
