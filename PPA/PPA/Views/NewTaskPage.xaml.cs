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

    /*
     * Corresponding file for .xaml frontend for new TaskItem
     */
    public partial class NewTaskPage : ContentPage
    {
        public NewTaskPage()
        {
            InitializeComponent();
        }
    }
}