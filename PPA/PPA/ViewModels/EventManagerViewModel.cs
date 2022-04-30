using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel; 
using Xamarin.Forms;
using PPA.Models;
using System.Threading.Tasks;
using PPA.Views;
using PPA.Services;
using System.Diagnostics;

namespace PPA.ViewModels
{
    public class EventManagerViewModel : CalendarBaseViewModel
    {
        #region Properties
        public ObservableRangeCollection<Event> Events { get; }
        public ObservableRangeCollection<DateTime> SelectedDates { get; }
        public ObservableRangeCollection<Event> SelectedEvents { get; }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        #endregion

        public AsyncCommand<Event> DeleteEventCommand { get; }
        public AsyncCommand AddEventCommand { get; }
        public AsyncCommand LoadEventsCommand { get; }

        IEventDataStore EventService;

        #region Constructors
        public EventManagerViewModel()
        {
            AddEventCommand = new AsyncCommand(OnAddEvent);
            LoadEventsCommand = new AsyncCommand(LoadEvents);
            DeleteEventCommand = new AsyncCommand<Event>(OnDeleteEvent);
            
            EventService = DependencyService.Get<IEventDataStore>();

            Events = new ObservableRangeCollection<Event>();
            SelectedDates = new ObservableRangeCollection<DateTime>(); 
            SelectedEvents = new ObservableRangeCollection<Event>();


            SelectedDates.CollectionChanged += SelectedDates_CollectionChanged;

        }
        #endregion

        #region Methods
        private void SelectedDates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SelectedEvents.ReplaceRange(Events.Where(x => SelectedDates.Any(y => x.DateTime.Date == y.Date)).OrderByDescending(x => x.DateTime));
        }

        private async Task OnAddEvent()
        {
            await Shell.Current.GoToAsync(nameof(NewEventPage));
        }

        async Task LoadEvents()
        {
            IsBusy = true;
#if DEBUG
            await Task.Delay(500);
#endif 
            Events.Clear();
            var events = await EventService.GetEventsAsync();
            foreach (var ev in events)
            {
                Events.Add(ev);
            }
            
           IsBusy = false;
        } 
        

        public async Task OnAppearing()
        {
            IsBusy = true;
            await LoadEvents();
        }

        private async Task OnDeleteEvent(Event ev)
        {
            await EventService.DeleteEventAsync(ev.Id);
            SelectedEvents.Remove(ev);
            await LoadEvents();
        }
        #endregion

    }
}