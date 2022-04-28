using PPA.ViewModels;
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