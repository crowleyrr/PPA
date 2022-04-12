using PPA.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class TaskDetailViewModel : ViewModelBase
    {
        private string name;
        private string description;
        public Guid Id { get; set; }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }


        ITaskDataStore TaskService;
        public async void LoadTaskId(Guid taskId)
        {
            try
            {
                var task = await TaskService.GetTaskAsync(taskId);
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
