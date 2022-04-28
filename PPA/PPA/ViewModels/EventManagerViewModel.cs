﻿using System;
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

        public AsyncCommand AddEventCommand { get; }
        public AsyncCommand LoadEventsCommand { get; }

        IEventDataStore EventService;

        #region Constructors
        public EventManagerViewModel()
        {
            AddEventCommand = new AsyncCommand(OnAddEvent);
            LoadEventsCommand = new AsyncCommand(LoadEvents);
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
        


        public void OnAppearing()
        {
         IsBusy = true;
        }
        #endregion

        /*        private Event _selectedEvent; 
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

                */
    }
}

/*
      #region Properties
      public static readonly Random Random = new Random();

      public ObservableRangeCollection<Event> Events { get; } = new ObservableRangeCollection<Event>()
      {
          new Event() { Title = "Bowling", Description = "Bowling with friends" },
          new Event() { Title = "Swimming", Description = "Swimming with friends" },
          new Event() { Title = "Kayaking", Description = "Kayaking with friends" },
          new Event() { Title = "Shopping", Description = "Shopping with friends" },
          new Event() { Title = "Hiking", Description = "Hiking with friends" },
          new Event() { Title = "Kareoke", Description = "Kareoke with friends" },
          new Event() { Title = "Dining", Description = "Dining with friends" },
          new Event() { Title = "Running", Description = "Running with friends" },
          new Event() { Title = "Traveling", Description = "Traveling with friends" },
          new Event() { Title = "Clubbing", Description = "Clubbing with friends" },
          new Event() { Title = "Learning", Description = "Learning with friends" },
          new Event() { Title = "Driving", Description = "Driving with friends" },
          new Event() { Title = "Skydiving", Description = "Skydiving with friends" },
          new Event() { Title = "Bungee Jumping", Description = "Bungee Jumping with friends" },
          new Event() { Title = "Trampolining", Description = "Trampolining with friends" },
          new Event() { Title = "Adventuring", Description = "Adventuring with friends" },
          new Event() { Title = "Roller Skating", Description = "Rollerskating with friends" },
          new Event() { Title = "Ice Skating", Description = "Ice Skating with friends" },
          new Event() { Title = "Skateboarding", Description = "Skateboarding with friends" },
          new Event() { Title = "Crafting", Description = "Crafting with friends" },
          new Event() { Title = "Drinking", Description = "Drinking with friends" },
          new Event() { Title = "Playing Games", Description = "Playing Games with friends" },
          new Event() { Title = "Canoeing", Description = "Canoeing with friends" },
          new Event() { Title = "Climbing", Description = "Climbing with friends" },
          new Event() { Title = "Partying", Description = "Partying with friends" },
          new Event() { Title = "Relaxing", Description = "Relaxing with friends" },
          new Event() { Title = "Exercising", Description = "Exercising with friends" },
          new Event() { Title = "Baking", Description = "Baking with friends" },
          new Event() { Title = "Skiing", Description = "Skiing with friends" },
          new Event() { Title = "Snowboarding", Description = "Snowboarding with friends" },
          new Event() { Title = "Surfing", Description = "Surfing with friends" },
          new Event() { Title = "Paragliding", Description = "Paragliding with friends" },
          new Event() { Title = "Sailing", Description = "Sailing with friends" },
          new Event() { Title = "Cooking", Description = "Cooking with friends" }
      };
      public ObservableRangeCollection<DateTime> SelectedDates { get; } = new ObservableRangeCollection<DateTime>();
      public ObservableRangeCollection<Event> SelectedEvents { get; } = new ObservableRangeCollection<Event>();
      #endregion

      public AsyncCommand AddEventCommand { get; }

      #region Constructors
      public EventManagerViewModel()
      {
          AddEventCommand = new AsyncCommand(OnAddEvent);

          foreach (Event Event in Events)
          {
              Event.DateTime = DateTime.Today.AddDays(Random.Next(-20, 21)).AddSeconds(Random.Next(86400));
          }

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
      #endregion

      */