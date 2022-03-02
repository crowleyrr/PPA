using PPA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    public class NewTaskViewModel : BaseViewModel
    {
        private string name;
        private string description;

        public NewTaskViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(description);
        }

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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // pop current page off navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            TaskItem newTask = new TaskItem()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Description = Description,
            };

            await DataStore.AddTaskAsync(newTask);
            await Shell.Current.GoToAsync("..");


        }

    }
}
