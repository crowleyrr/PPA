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
    /*
     * Backend to creating a new Reminder
     */
    public class NewReminderViewModel : ViewModelBase
    {
        // variables
        private string name;
        private DateTime datetime;
        private DateTime date;
        private TimeSpan time;
        public string ReminderName { get => name; set => SetProperty(ref name, value); }
        public DateTime DateTime { get => datetime; set => SetProperty(ref datetime, value); }
        public DateTime ReminderDate { get => date; set => SetProperty(ref date, value); }
        public TimeSpan ReminderTime { get => time; set => SetProperty(ref time, value); }


        // commands utilized
        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }

        // connect to Reminder database
        IReminderDataStore ReminderService;

        /*
         * New Reminder constructor
         */
        public NewReminderViewModel()
        {
            SaveCommand = new AsyncCommand(OnSave);
            CancelCommand = new AsyncCommand(OnCancel);
            ReminderService = DependencyService.Get<IReminderDataStore>();
        }

        /*
         * Executes if cancel button pressed
         * Returns user to Reminder manager page
         */
        async Task OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        /*
         * Executes if save button pressed
         * Verifies Reminder object fields
         * If valid, then Reminder is added to database
         * Creates notifications for Reminder
         * Returns to Reminder manager page
         */
        private async Task OnSave()
        {

            if (String.IsNullOrWhiteSpace(name)
                || String.IsNullOrWhiteSpace(datetime.ToString()))
            {
                return;
            }

            Reminder newReminder = new Reminder()
            {
                ReminderName = name,
                ReminderTime = ReminderDate.Date.Add(ReminderTime),
            };

            // create notification for new Reminder
            await NotificationCenter.Current.Show((notification) => notification
            .WithScheduleOptions((schedule) => schedule
                    .NotifyAt(ReminderDate.Date.Add(ReminderTime))
                    .Build())
                        .WithTitle("Don't forget:")
                        .WithDescription(ReminderName)
                        .WithNotificationId(100)
                        .Create());

            // Add to database and return to Reminder manager
            await ReminderService.AddReminderAsync(newReminder);
            await Shell.Current.GoToAsync("..");
        }
    }
}