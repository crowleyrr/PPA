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
    /*
     * Corresponding file for .xaml frontend of Task Manager
     */
    public partial class TaskManagerPage : ContentPage
    {
        TaskManagerViewModel _viewModel;
        public TaskManagerPage()
        {
            InitializeComponent();
            // initialize context for page
            BindingContext = _viewModel = new TaskManagerViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}