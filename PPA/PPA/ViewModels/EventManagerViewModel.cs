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
    public class EventManagerViewModel : ViewModelBase
    {
/*        private Event _selectedEvent; */
        public ObservableCollection<Event> Events { get; set; }
        public AsyncCommand LoadEventsCommand { get; }
        public AsyncCommand AddEventCommand { get; }
        public AsyncCommand<Event> EventTapped { get; }
  

        IEventDataStore EventService;

        public EventManagerViewModel()
        {
            Events = new ObservableCollection<Event>();
            LoadEventsCommand = new AsyncCommand(LoadEvents);
            EventTapped = new AsyncCommand<Event>(OnEventSelected);
            AddEventCommand = new AsyncCommand(OnAddEvent);
        }

        async Task LoadEvents()
        {
            IsBusy = true;
            try
            {
                Events.Clear();
                var events = await EventService.GetEventsAsync();
                foreach (var ev in events)
                {
                    Events.Add(ev);
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
        }

        private async Task OnAddEvent()
        {
            await Shell.Current.GoToAsync(nameof(NewEventPage));
        }

        async Task OnEventSelected(Event ev){
            if (ev == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(EventDetailPage)}?EventId={ev.Id}");
        }
    }
}

/*
 * 
 * using PPA.Models;
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
    public class EventManagerViewModel : BaseViewModel
    {
        private Event _selectedEvent;
        public ObservableCollection<Event> Events { get; set; }
        public Command LoadEventsCommand { get; }
        public Command AddEventCommand { get; }
        public Command<Event> EventTapped { get; }


        public EventManagerViewModel()
        {
            Events = new ObservableCollection<Event>();
            LoadEventsCommand = new Command(async () => ExecuteLoadEventsCommand());
            EventTapped = new Command<Event>(OnEventSelected);
            AddEventCommand = new Command(OnAddEvent);
        }

        async Task ExecuteLoadEventsCommand()
        {
            IsBusy = true;
            try
            {
                Events.Clear();
                var events = await DataStore.GetEventsAsync(true);
                foreach (var ev in events)
                {
                    Events.Add(ev);
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
            _selectedEvent = null;
        }

        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                SetProperty(ref _selectedEvent, value);
                OnEventSelected(value);
            }
        }

        private async void OnAddEvent(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewEventPage));
        }

        async void OnEventSelected(Event ev){
            if (ev == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(EventDetailPage)}?{nameof(EventDetailViewModel.EventId)}={ev.Id}");
        }
    }
}
*/