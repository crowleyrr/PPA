using PPA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class EventManagerPage : ContentPage
    {
        EventManagerViewModel _viewModel;
        public EventManagerPage()
        {
            try {
                InitializeComponent();
                BindingContext = _viewModel = new EventManagerViewModel();
            } catch(System.Exception ex)
            {

                DisplayAlert("Alert", ex.ToString(), "OK");
            }
        }
    }
}