using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class TaskDetailViewModel : BaseViewModel
    {
        private string taskId;
        private string name;
        private string description;
        public string Id { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string TaskId
        {
            get
            {
                return taskId;
            }
            set
            {
                taskId = value;
                LoadTaskId(value);
            }
        }

        public async void LoadTaskId(string taskId)
        {
            try
            {
                var task = await DataStore.GetTaskAsync(taskId);
                Id = task.Id;
                Name = task.Name;
                Description = task.Description;
            } 
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Task");
            }
        }
    }
}
