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
    public partial class ReminderManagerPage : ContentPage
    {
        ReminderManagerViewModel _viewModel;

        /*
         * corresponding file for .xaml frontend of Reminder manager
         */
        public ReminderManagerPage()
        {
            InitializeComponent();
            // initialize context for page
                BindingContext = _viewModel = new ReminderManagerViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}