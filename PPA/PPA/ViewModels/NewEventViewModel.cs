using MvvmHelpers.Commands;
using Plugin.LocalNotification;
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
    /*
     * Backend to add a new Event
     */
    public class NewEventViewModel : ViewModelBase
    {
       
        // variables needed
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
  

        // commands to add or cancel a new event
        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }

        // Connect to the Event database 
        IEventDataStore EventService;

        /*
         * NewEventViewModel constructor
         */
        public NewEventViewModel()
        {
            SaveCommand = new AsyncCommand(OnSave);
            CancelCommand = new AsyncCommand(OnCancel);
            EventService = DependencyService.Get<IEventDataStore>();
             
        }

        /*
         * Verifies potential new Event
         * Adds new Event to database if valid
         * Creates notifications for Event at time-of and 30 minutes before
         * Returns to Event Manager page once complete
         */
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

            // create notification for time-of
            await NotificationCenter.Current.Show((notification) => notification
           .WithScheduleOptions((schedule) => schedule
                   .NotifyAt(EventDate.Date.Add(EventTime))
                   .Build())
                       .WithTitle("Happening now: " + EventTitle)
                       .WithDescription(EventDescription)
                       .WithNotificationId(100)
                       .Create());

            // create notification for 30 minutes before event
            await NotificationCenter.Current.Show((notification) => notification
          .WithScheduleOptions((schedule) => schedule
                  .NotifyAt(EventDate.Date.Add(EventTime).AddMinutes(-30))
                  .Build())
                      .WithTitle("Happening soon: " + EventTitle + " at " + EventDate.Date.Add(EventTime))
                      .WithDescription(EventDescription)
                     // .WithReturningData("Dummy Data")
                      .WithNotificationId(100)
                      .Create());

            //Add to database and then return to Event Manager page
            await EventService.AddEventAsync(ev);
            await Shell.Current.GoToAsync("..");
        }

        /*
         * If cancel button is pressed, then return to Event Manager page
         */
        async Task OnCancel()
        {
            await Shell.Current.GoToAsync("..");

        }


    }
}