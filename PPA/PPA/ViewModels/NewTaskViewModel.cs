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
    /*
     * Backend to add a new TaskItem 
     */
    public class NewTaskViewModel : ViewModelBase
    {

        // variables
        private string name;
        private string description;
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }

        // Command initialization
        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }

        // connect to database
        ITaskDataStore TaskService; 

        /*
         * Constructor
         */
        public NewTaskViewModel()
        {
            SaveCommand = new AsyncCommand(OnSave);
            CancelCommand = new AsyncCommand(OnCancel);
            TaskService = DependencyService.Get<ITaskDataStore>(); 
        }

        /*
         * Executes when cancel button pushed
         * Returns user to Task Manager page
         */
       async Task OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        /*
         * Executes when save button pushed
         * Verifies TaskItem fields
         * Adds new TaskItem to database
         * Returns user to Task Manager page
         */
        async Task OnSave()
        {
            if (String.IsNullOrWhiteSpace(name)
                || String.IsNullOrWhiteSpace(description))
            {
                return;
            }

            TaskItem newTask = new TaskItem()
            {
                Name = Name,
                Description = Description,
                Today = true,
            };

            // Add to database and return to Task Manager page
            await TaskService.AddTaskAsync(newTask);
            await Shell.Current.GoToAsync("..");


        }

    }
}
