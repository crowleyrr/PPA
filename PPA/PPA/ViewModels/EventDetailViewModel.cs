using PPA.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PPA.ViewModels
{
    [QueryProperty(nameof(EventName), nameof(EventName))]
    public class EventDetailViewModel : ViewModelBase
    {
        private string eventName;
        private string eventLocation;
        private DateTime eventStartTime;
        private DateTime eventEndTime;

        public int Id { get; set; }
        public string EventName{  get => eventName; set => SetProperty(ref eventName, value); }
        public string EventLocation { get => eventLocation; set => SetProperty(ref eventLocation, value); }
        public DateTime EventStartTime { get => eventStartTime; set => SetProperty(ref eventStartTime, value); }
        public DateTime EventEndTime { get => eventEndTime; set => SetProperty(ref eventEndTime, value); }
   

        IEventDataStore EventService;
        public async void LoadEventId(int eventId)
        {
            try
            {
                var ev = await EventService.GetEventAsync(eventId);
                EventName = ev.EventName;
                EventLocation = ev.EventLocation;
                EventStartTime = ev.EventStartTime;
                EventEndTime = ev.EventEndTime;
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