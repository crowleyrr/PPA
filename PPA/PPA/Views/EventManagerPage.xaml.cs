using PPA.ViewModels;
using PPA.Models;
using PPA.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPA.Views
{
    public partial class EventManagerPage : ContentPage
    {
        public EventManagerPage()
        {
            InitializeComponent();
            BindingContext = new EventManagerViewModel();
        }

        private void Cal_DateSelectionChanged(object sender, EventArgs arg)
        {
            DisplayAlert("Date Changed", calendar.SelectedDates.ToString(), "OK");
        }
    }
}