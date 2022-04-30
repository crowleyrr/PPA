using MvvmHelpers.Commands;
using PPA.Models;
using PPA.Services;
using PPA.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    /*
     * Backend for main Reminder page
     */
    public class ReminderManagerViewModel : ViewModelBase
    {        
        // initialize variables
        public ObservableCollection<Reminder> Reminders { get; }

        //initialize commands
        public AsyncCommand LoadRemindersCommand { get; }
        public AsyncCommand AddReminderCommand { get;  }
        public AsyncCommand<Reminder> DeleteReminderCommand { get; }
        public AsyncCommand<Reminder> SnoozeReminderCommand { get; }

        // connect to database
        IReminderDataStore ReminderService;

        /*
         * Constructor
         */
        public ReminderManagerViewModel()
        {
            Reminders = new ObservableCollection<Reminder>();
            LoadRemindersCommand = new AsyncCommand(LoadReminders);
            AddReminderCommand = new AsyncCommand(OnAddReminder);
            DeleteReminderCommand = new AsyncCommand<Reminder>(OnDeleteReminder);
            SnoozeReminderCommand = new AsyncCommand<Reminder>(SnoozeReminder);

            ReminderService = DependencyService.Get<IReminderDataStore>();
        }

        /*
         * Loads reminders to be displayed on page
         */
        async Task LoadReminders()
        {
            IsBusy = true;
              Reminders.Clear();
                var reminders = await ReminderService.GetRemindersAsync();
                foreach(var reminder in reminders)
                {
                    Reminders.Add(reminder);
                }
          
                IsBusy = false;
        }

        /*
         * Method for when page appears
         */
        public void OnAppearing()
        {
            IsBusy = true;
        }

        /*
         * Executed when 'Add' button pushed
         * Displays New Reminder page
         */
        private async Task OnAddReminder()
        {
            await Shell.Current.GoToAsync(nameof(NewReminderPage));
        }

        /*
         * Deletes the Reminder of which 'Delete' button was pushed
         * Reloads Reminders
         */
        private async Task OnDeleteReminder(Reminder reminder)
        {
            await ReminderService.DeleteReminderAsync(reminder.Id);
            await LoadReminders();

        }

        /*
         * Pushes Reminder back by 1 hour
         * Reloads Reminders to reflect change
         * 
         * NOTE: I know it's not ideal to delete existing and create a new object,
         * but this is the only way I could get it to work for some reason.
         */
        private async Task SnoozeReminder(Reminder reminder)
        {
            Reminder newReminder = new Reminder()
            {
                ReminderName = reminder.ReminderName,
                ReminderTime = reminder.ReminderTime.AddHours(1),
            };
            await OnDeleteReminder(reminder);
            await ReminderService.AddReminderAsync(newReminder);
            await LoadReminders();
        }


    }
}