using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PPA.Models;
using PPA.Views;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    public class ReminderManagerViewModel : BaseViewModel
    {
        private Reminder _selectedReminder;
        
        public ObservableCollection<Reminder> Reminders { get; }
        public Command LoadRemindersCommand { get; }
        public Command AddReminderCommand { get;  }
        public Command<Reminder> ReminderTapped { get; }

        public ReminderManagerViewModel()
        {
            Reminders = new ObservableCollection<Reminder>();
            LoadRemindersCommand = new Command(async () => await ExecuteLoadRemindersCommand());
            ReminderTapped = new Command<Reminder>(OnReminderSelected);
            AddReminderCommand = new Command(OnAddReminder);
        }

        async Reminder ExecuteLoadRemindersCommand()
        {
            IsBusy = true;

            try
            {
                Reminders.Clear();
                var reminders = await DataStore.GetRemindersAsync(true);
                foreach (var reminder in reminders)
                {
                    Reminders.Add(reminder);
                }
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            _selectedReminder = null;
        }

        public Reminder SelectedReminder
        {
            get => _selectedReminder;
            set
            {
                SetProperty(ref _selectedReminder, value);
                OnReminderSelected(value);
            }
        }

        private async void OnAddReminder(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewReminderPage));
        }

        async void OnReminderSelected(Reminder reminder)
        {
            if (reminder == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ReminderDetailPage)}?{nameof(ReminderDetailViewModel.ReminderId)}={reminder.Id}")
        }
    }
}