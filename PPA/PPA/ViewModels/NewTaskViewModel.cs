using MvvmHelpers.Commands;
using PPA.Models;
using PPA.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    public class NewTaskViewModel : ViewModelBase
    {
        private string name;
        private string description;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }

        ITaskDataStore TaskService; 
        public NewTaskViewModel()
        {
            SaveCommand = new AsyncCommand(OnSave);
            CancelCommand = new AsyncCommand(OnCancel);
            TaskService = DependencyService.Get<ITaskDataStore>(); 

          /*  this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute(); */
        }
       async Task OnCancel()
        {
            // pop current page off navigation stack
            await Shell.Current.GoToAsync("..");
        }

        async Task OnSave()
        {
            if (String.IsNullOrWhiteSpace(name)
                && String.IsNullOrWhiteSpace(description))
            {
                return;
            }

            TaskItem newTask = new TaskItem()
            {
                Name = Name,
                Description = Description,
                Today = true,
            };

            await TaskService.AddTaskAsync(newTask);
            await Shell.Current.GoToAsync("..");


        }

    }
}
