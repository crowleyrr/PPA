using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using PPA.Models;
using PPA.Services;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    public class NewReminderViewModel : ViewModelBase
    {
        private string name;
        private DateTime time;

        public string ReminderName { get => name; set => SetProperty(ref name, value); }
        public DateTime ReminderTime { get => time; set => SetProperty(ref time, value); }

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }

        IReminderDataStore ReminderService;
        public NewReminderViewModel()
        {
            SaveCommand = new AsyncCommand(OnSave);
            CancelCommand = new AsyncCommand(OnCancel);
            ReminderService = DependencyService.Get<IReminderDataStore>();
           /* this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute(); */
        }

        async Task OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSave()
        {
            if (String.IsNullOrWhiteSpace(name)
                && String.IsNullOrWhiteSpace(time.ToString()))
            {
                return;
            }

            var newReminder = new Reminder()
            {
                ReminderName = ReminderName,
                ReminderTime = ReminderTime,
            };

            await ReminderService.AddReminderAsync(newReminder);
            await Shell.Current.GoToAsync("..");
        }
    }
}