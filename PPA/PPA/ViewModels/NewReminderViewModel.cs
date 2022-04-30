using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers.Commands;
using Plugin.LocalNotification;
using PPA.Models;
using PPA.Services;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    public class NewReminderViewModel : ViewModelBase
    {
        private string name;
        private DateTime datetime;
        private DateTime date;
        private TimeSpan time;

        public string ReminderName { get => name; set => SetProperty(ref name, value); }
        public DateTime DateTime { get => datetime; set => SetProperty(ref datetime, value); }
        public DateTime ReminderDate { get => date; set => SetProperty(ref date, value); }
        public TimeSpan ReminderTime { get => time; set => SetProperty(ref time, value); }

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
            
            // pop current page off navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSave()
        {

            if (String.IsNullOrWhiteSpace(name)
                && String.IsNullOrWhiteSpace(datetime.ToString()))
            {
                return;
            }

            Reminder newReminder = new Reminder()
            {
                ReminderName = name,
                ReminderTime = ReminderDate.Date.Add(ReminderTime),
            };
            await ReminderService.AddReminderAsync(newReminder);

            await NotificationCenter.Current.Show((notification) => notification
            .WithScheduleOptions((schedule) => schedule
                    .NotifyAt(ReminderDate.Date.Add(ReminderTime)) // Used for Scheduling local notification, if not specified notification will show immediately.
                    .Build())
                        .WithTitle("Don't forget:")
                        .WithDescription(ReminderName)
                        .WithReturningData("Dummy Data") // Returning data when tapped on notification.
                        .WithNotificationId(100)
                        .Create());
            await Shell.Current.GoToAsync("..");
        }
    }
}