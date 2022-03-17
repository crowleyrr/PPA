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
        private string name;
        private string location;
        private DateTime startTime;
        private DateTime endTime;

        public string EventName { get => name; set => SetProperty(ref name, value); }
        public string EventLocation { get => location; set => SetProperty(ref location, value); }
        public DateTime EventStartTime { get => startTime; set => SetProperty(ref startTime, value); }
        public DateTime EventEndTime { get => endTime; set => SetProperty(ref endTime, value); }

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }
        IEventDataStore EventService;

        public NewEventViewModel()
        {
            SaveCommand = new AsyncCommand(OnSave);
            CancelCommand = new AsyncCommand(OnCancel);
            EventService = DependencyService.Get<IEventDataStore>();
            /*this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute(); */
        }

        async Task OnSave()
        {
            if(String.IsNullOrWhiteSpace(name)
                && String.IsNullOrWhiteSpace(location)
                && String.IsNullOrWhiteSpace(startTime.ToString())
                && String.IsNullOrWhiteSpace(endTime.ToString()))
            {
                return;
            }

            var ev = new PPA.Models.Event
            {
                EventName = name,
                EventLocation = location,
                EventStartTime = startTime,
                EventEndTime = endTime,
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