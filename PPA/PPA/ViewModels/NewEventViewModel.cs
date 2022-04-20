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
        private Color color;
        private string colorName;


        public string EventTitle { get => title; set => SetProperty(ref title, value); }
        public string EventDescription { get => description; set => SetProperty(ref description, value); }
        public DateTime EventDatetime { get => datetime; set => SetProperty(ref datetime, value); }
        public Color EventColor { get => color; set => SetProperty(ref color, value); }

        public string ColorName { get => colorName; set => SetProperty(ref colorName, value); }


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
                || String.IsNullOrWhiteSpace(datetime.ToString())
                || String.IsNullOrWhiteSpace(color.ToString()))
            {
                return;
            }

            switch (ColorName)
            {
                case "Red":
                    color = Color.Red;
                    break;
                case "Orange":
                    color = Color.Orange;
                    break;
                case "Yellow":
                    color = Color.Yellow;
                    break;
                case "Green":
                    color = Color.FromHex("#00A000");
                    break;
                case "Blue":
                    color = Color.Blue;
                    break;
                case "Purple":
                    color = Color.FromHex("#8010E0");
                    break;
                default:
                    color = Color.Red;
                    break;
            }

            var ev = new PPA.Models.Event
            {
                Title = title,
                Description = description,
                DateTime = datetime,
                Color  = color,
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