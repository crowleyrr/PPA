using PPA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class EventManagerPage : ContentPage
    {
        public EventManagerPage()
        {
            try { 
            InitializeComponent();
            BindingContext = new EventManagerViewModel();
            } catch(System.Exception ex)
            {

                DisplayAlert("Alert", ex.ToString(), "OK");
            }
        }
    }
}