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
    public partial class NewReminderPage : ContentPage
    {
        public Reminder Reminder { get; set; }
        public NewReminderPage()
        {
            InitializeComponent();
            BindingContext = new NewReminderViewModel();
        }
    }
}