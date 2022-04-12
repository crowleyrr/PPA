using MvvmHelpers.Commands;
using PPA.Models;
using PPA.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    internal class TomorrowTaskViewModel: ViewModelBase
    {
        public ObservableCollection<TomorrowTaskItem> Tasks { get; }
        public AsyncCommand LoadTasksCommand { get; }
        public AsyncCommand<TomorrowTaskItem> DeleteTaskCommand { get; }


         ITomorrowTaskDataStore TomorrowTaskService;
        public TomorrowTaskViewModel()
        {
            Tasks = new ObservableCollection<TomorrowTaskItem>();
            LoadTasksCommand = new AsyncCommand(LoadTasks);
            TomorrowTaskService = DependencyService.Get<ITomorrowTaskDataStore>();
            DeleteTaskCommand = new AsyncCommand<TomorrowTaskItem>(OnDeleteTask);


        }

        async Task LoadTasks()
        {
            IsBusy = true;

#if DEBUG
            await Task.Delay(500);
#endif 
            Tasks.Clear();
            var tasks = await TomorrowTaskService.GetTasksAsync(); 
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
            
            IsBusy = false;
        }

        private async Task OnDeleteTask(TomorrowTaskItem task)
        {
            await TomorrowTaskService.DeleteTaskAsync(task.Id);
            await LoadTasks();
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

    }
}
