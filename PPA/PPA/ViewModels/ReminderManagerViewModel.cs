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
    public class ReminderManagerViewModel : ViewModelBase
    {        
        public ObservableCollection<Reminder> Reminders { get; }
        public AsyncCommand LoadRemindersCommand { get; }
        public AsyncCommand AddReminderCommand { get;  }
        public AsyncCommand<Reminder> DeleteReminderCommand { get; }
        public AsyncCommand<Reminder> SnoozeReminderCommand { get; }


        IReminderDataStore ReminderService;
        public ReminderManagerViewModel()
        {
            Reminders = new ObservableCollection<Reminder>();
            LoadRemindersCommand = new AsyncCommand(LoadReminders);
            AddReminderCommand = new AsyncCommand(OnAddReminder);
            DeleteReminderCommand = new AsyncCommand<Reminder>(OnDeleteReminder);
            SnoozeReminderCommand = new AsyncCommand<Reminder>(SnoozeReminder);

            ReminderService = DependencyService.Get<IReminderDataStore>();
        }

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

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async Task OnAddReminder()
        {
            await Shell.Current.GoToAsync(nameof(NewReminderPage));
        }

        private async Task OnDeleteReminder(Reminder reminder)
        {
            await ReminderService.DeleteReminderAsync(reminder.Id);
            await LoadReminders();

        }

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