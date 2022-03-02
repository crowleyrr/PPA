using PPA.Models;
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