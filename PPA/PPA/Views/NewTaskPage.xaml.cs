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
    public partial class NewTaskPage : ContentPage
    {
        public TaskItem Task { get; set; }
        public NewTaskPage()
        {
            InitializeComponent();
            BindingContext = new NewTaskViewModel();
        }
    }
}