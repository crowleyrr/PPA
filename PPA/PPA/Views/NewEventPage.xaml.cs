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
     * Corresponding code to .xaml frontend for new Event
     */
    public partial class NewEventPage : ContentPage
    {
        public NewEventPage()
        {
            InitializeComponent();
            // don't allow events to be created for the past
            datePicker.MinimumDate = DateTime.Now.Date; 

            // initialize the datepicker and timepicker to the current
            datePicker.Date = DateTime.Now.Date;
            timePicker.Time = DateTime.Now.TimeOfDay;
        }
    }
}