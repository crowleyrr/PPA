using MvvmHelpers.Commands;
using PPA.Models;
using PPA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    public class NewEventViewModel : ViewModelBase
    {
       
        private string title;
        private string description;
        private DateTime datetime;
        private DateTime date;
        private TimeSpan time;



        public string EventTitle { get => title; set => SetProperty(ref title, value); }
        public string EventDescription { get => description; set => SetProperty(ref description, value); }
        public DateTime DateTime { get => datetime; set => SetProperty(ref datetime, value); }
        public DateTime EventDate { get => date; set => SetProperty(ref date, value); }
        public TimeSpan EventTime { get => time; set => SetProperty(ref time, value); }
  


        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }
        IEventDataStore EventService;

        public NewEventViewModel()
        {
            SaveCommand = new AsyncCommand(OnSave);
            CancelCommand = new AsyncCommand(OnCancel);
            EventService = DependencyService.Get<IEventDataStore>();
             
        }

        async Task OnSave()
        {
            if(String.IsNullOrWhiteSpace(title)
                || String.IsNullOrWhiteSpace(description)
                || String.IsNullOrWhiteSpace(datetime.ToString()))
            {
                return;
            }

            var ev = new PPA.Models.Event
            {
                Title = title,
                Description = description,
                DateTime = EventDate.Date.Add(EventTime),
            };

            await EventService.AddEventAsync(ev);
            await Shell.Current.GoToAsync("..");
        }

        async Task OnCancel()
        {
            await Shell.Current.GoToAsync("..");

        }


    }
}

/*
 * using PPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PPA.ViewModels
{
    public class NewEventViewModel : BaseViewModel
    {
        private string name;
        private string location;
        private DateTime startTime;
        private DateTime endTime;
        public NewEventViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(location)
                && !String.IsNullOrWhiteSpace(startTime.ToString())
                && !String.IsNullOrWhiteSpace(endTime.ToString());
        }

        public string EventName
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string EventLocation
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        public DateTime EventStartTime
        {
              get => startTime;
            set => SetProperty(ref startTime, value);
        }

        public DateTime EventEndTime
        {
            get => endTime;
            set => SetProperty(ref endTime, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Event newEvent = new Event()
            {
                Id = Guid.NewGuid().ToString(),
                EventName = EventName,
                EventLocation = EventLocation,
                EventStartTime = EventStartTime,
                EventEndTime = EventEndTime,
            };

            await DataStore.AddEventAsync(newEvent);
            await Shell.Current.GoToAsync("..");
        }
    }
}
*/