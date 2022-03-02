using PPA.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PPA
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewTaskPage), typeof(NewTaskPage));
        }

    }
}
