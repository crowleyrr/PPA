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
    /*Calendar license info:
     * 
     * https://github.com/ME-MarvinE/XCalendar
     * 
     * MIT License

        Copyright (c) 2022 MarvinE

        Permission is hereby granted, free of charge, to any person obtaining a copy
        of this software and associated documentation files (the "Software"), to deal
        in the Software without restriction, including without limitation the rights
        to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        copies of the Software, and to permit persons to whom the Software is
        furnished to do so, subject to the following conditions:

        The above copyright notice and this permission notice shall be included in all
        copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
        AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
        SOFTWARE.
     * 
     */

    /*
     * Backend for Event Manager page
     */
    public class EventManagerViewModel : CalendarBaseViewModel
    {
        //Properties
        public ObservableRangeCollection<Event> Events { get; }
        public ObservableRangeCollection<DateTime> SelectedDates { get; }
        public ObservableRangeCollection<Event> SelectedEvents { get; }

        private bool _isBusy;
        public bool 
            IsBusy
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

        // Commands
        public AsyncCommand<Event> DeleteEventCommand { get; }
        public AsyncCommand AddEventCommand { get; }
        public AsyncCommand LoadEventsCommand { get; }

        // Connect to database
        IEventDataStore EventService;

        /*
         * Event Manager constructor
         */
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

        /*
         * When a date is selected, add the events from that date 
         * to the ones to be displayed
         */
        private void SelectedDates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SelectedEvents.ReplaceRange(Events.Where(x => SelectedDates.Any(y => x.DateTime.Date == y.Date)).OrderByDescending(x => x.DateTime));
        }

        /*
         * Go to the New Event page
         */
        private async Task OnAddEvent()
        {
            await Shell.Current.GoToAsync(nameof(NewEventPage));
        }

        /*
         * Load events from the datastore
         */
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
        

        /*
         * Method for when Event manager page appears
         */
        public async Task OnAppearing()
        {
            IsBusy = true;
            await LoadEvents();
        }

        /*
         * Deletes requested event based on ID
         */
        private async Task OnDeleteEvent(Event ev)
        {
            await EventService.DeleteEventAsync(ev.Id);
            SelectedEvents.Remove(ev);
            await LoadEvents();
        }

    }
}