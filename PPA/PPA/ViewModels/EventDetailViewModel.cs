using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PPA.ViewModels
{
    [QueryProperty(nameof(EventId), nameof(EventId))]
    public class EventDetailViewModel : BaseViewModel
    {
        private string eventId;
        private string eventName;
        private string eventLocation;
        private DateTime eventStartTime;
        private DateTime eventEndTime;

        public string Id { get; set; }

        public string EventName
        {
            get => eventName;
            set => SetProperty(ref eventName, value);  
        }

        public string EventLocation
        {
            get => eventLocation;
            set => SetProperty(ref eventLocation, value);
        }

        public DateTime EventStartTime
        {
            get => eventStartTime;
            set => SetProperty(ref eventStartTime, value);
        }

        public DateTime EventEndTime
        {
            get => eventEndTime;
            set => SetProperty(ref eventEndTime, value);
        }

        public string EventId
        {
            get
            {
                return eventId;
            }
            set
            {
                eventId = value;
                LoadEventId(value);
            }
        }

        public async void LoadEventId(string eventId)
        {
            try
            {
                var ev = await DataStore.GetEventAsync(eventId);
                Id = ev.Id;
                EventName = ev.EventName;
                EventLocation = ev.Location;
                EventStartTime = ev.StartTime;
                EventEndTime = ev.EndTime;
            } catch (Exception)
            {
                Debug.WriteLine("Failed to Load Event");
            }
        }

        /*
         *        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
         * 
        public EventDetailViewModel()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }
        */
    }
}