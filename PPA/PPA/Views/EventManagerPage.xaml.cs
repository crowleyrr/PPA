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
        EventManagerViewModel _viewModel;
        public EventManagerPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EventManagerViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}