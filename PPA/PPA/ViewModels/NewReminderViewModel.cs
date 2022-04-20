using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private DateTime datetime;

        public string ReminderName { get => name; set => SetProperty(ref name, value); }
        public DateTime ReminderTime { get => datetime; set => SetProperty(ref datetime, value); }

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

        public async Task OnSave()
        {
            if (String.IsNullOrWhiteSpace(name)
                || String.IsNullOrWhiteSpace(datetime.ToString()))
            {
                return;
            }

            try
            {
                Reminder newReminder = new Reminder()
                {
                    ReminderName = name,
                    ReminderTime = datetime,
                };
                Console.WriteLine(newReminder);
                await ReminderService.AddReminderAsync(newReminder);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRORRRRRRRRRRRR");
               Debug.WriteLine(ex);
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}