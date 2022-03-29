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
        public AsyncCommand<Reminder> ReminderTapped { get; }

        IReminderDataStore ReminderService;
        public ReminderManagerViewModel()
        {
            Reminders = new ObservableCollection<Reminder>();
            LoadRemindersCommand = new AsyncCommand(LoadReminders);
            ReminderTapped = new AsyncCommand<Reminder>(OnReminderSelected);
            AddReminderCommand = new AsyncCommand(OnAddReminder);
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

        async Task OnReminderSelected(Reminder reminder)
        {
            if (reminder == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ReminderDetailPage)}?ReminderId={reminder.Id}");
        }
    }
}