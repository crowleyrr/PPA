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
    public partial class TaskDetailPage : ContentPage
    {
        public TaskDetailPage()
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel();
        }
    }
}

/*
 * namespace PPA.Views
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class TaskDetailPage : ContentPage
    {
        public int Id { get; set; }
        ITaskDataStore TaskService;
        public TaskDetailPage()
        {
            InitializeComponent();
            TaskService = DependencyService.Get<ITaskDataStore>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = await TaskService.GetTaskAsync(Id);

        }
    }
}*/