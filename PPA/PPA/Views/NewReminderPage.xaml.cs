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

    public partial class NewReminderPage : ContentPage
    {
        public NewReminderPage()
        {
            InitializeComponent();
            datePicker.MinimumDate = DateTime.Now.Date;
            datePicker.Date = DateTime.Now.Date;
            timePicker.Time = DateTime.Now.TimeOfDay;
        }

    }
}