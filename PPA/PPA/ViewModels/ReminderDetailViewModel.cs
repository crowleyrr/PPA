using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PPA.ViewModels
{
    [QueryProperty(nameof(ReminderId), nameof(ReminderId))]
    public class ReminderDetailViewModel : BaseViewModel
    {
        private string reminderId;
        private string name;
        private DateTime time;
        public string Id { get; set; }


        public string ReminderName
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public DateTime ReminderTime
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public string ReminderId
        {
            get
            {
                return reminderId;
            }
            set
            {
                reminderId = value;
                LoadReminderId(value);
            }
        }

        public async void LoadReminderId(string reminderId)
        {
            try
            {
                var reminder = await DataStore.GetReminderAsync(reminderId);
                Id = reminder.Id;
                ReminderName = reminder.ReminderName;
                ReminderTime = reminder.ReminderTime;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Reminder");
            }
        }


        /*
         * 
        public ReminderDetailViewModel()
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