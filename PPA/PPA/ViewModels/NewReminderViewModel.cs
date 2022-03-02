using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PPA.Models;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    public class NewReminderViewModel : BaseViewModel
    {
        private string name;
        private DateTime time;
        public NewReminderViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(time.ToString());
        }

        public string ReminderName
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public DateTime ReminderTime
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Reminder newReminder = new Reminder()
            {
                Id = Guid.NewGuid().ToString(),
                ReminderName = ReminderName,
                ReminderTime = ReminderTime,
            };

            await DataStore.AddReminderAsync(newReminder);
            await Shell.Current.GoToAsync("..");
        }
    }
}