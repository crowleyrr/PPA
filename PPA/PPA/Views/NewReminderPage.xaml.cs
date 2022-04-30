using PPA.Models;
using PPA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /*
     * Corresponding file to .xaml frontend to add new Reminder
     */
     public partial class NewReminderPage : ContentPage
    {
        public NewReminderPage()
        {
            InitializeComponent();

            // don't allow reminders to be set for previous days
            datePicker.MinimumDate = DateTime.Now.Date;

            // initialize datepicker and timepicker to be current
            datePicker.Date = DateTime.Now.Date;
            timePicker.Time = DateTime.Now.TimeOfDay;
        }

    }
}