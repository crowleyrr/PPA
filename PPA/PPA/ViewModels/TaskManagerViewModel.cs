using MvvmHelpers.Commands;
using MvvmHelpers;
using PPA.Models;
using PPA.Services;
using PPA.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace PPA.ViewModels
{
    public class TaskManagerViewModel: ViewModelBase
    {
        public ObservableCollection<TaskItem> Tasks { get; }
        public AsyncCommand LoadTasksCommand { get; } 
        public AsyncCommand AddTaskCommand  { get; }
        public AsyncCommand<TaskItem> TaskTapped { get; }
        public AsyncCommand<TaskItem> DeleteTaskCommand { get; }


        ITaskDataStore TaskService;
        public TaskManagerViewModel()
        {
            Title = "Task Manager";
            Tasks = new ObservableCollection<TaskItem>();
            LoadTasksCommand = new AsyncCommand(LoadTasks); 
            TaskTapped = new AsyncCommand<TaskItem>(OnTaskSelected);
            AddTaskCommand = new AsyncCommand(OnAddTask);
            DeleteTaskCommand = new AsyncCommand<TaskItem>(OnDeleteTask);

            TaskService = DependencyService.Get<ITaskDataStore>();


        }

        async Task LoadTasks()
        {
            IsBusy = true;

#if DEBUG
            await Task.Delay(500);
#endif 
            Tasks.Clear();
            var tasks = await TaskService.GetTasksAsync();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }

            IsBusy = false;
        } 


        private async Task OnAddTask()
        {
            await Shell.Current.GoToAsync(nameof(NewTaskPage));
        }

        private async Task OnDeleteTask(TaskItem task)
        {
            await TaskService.DeleteTaskAsync(task.Id);
            await LoadTasks();
        }

        async Task OnTaskSelected(TaskItem task)
        {
            if(task == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TaskDetailPage)}?TaskId={task.Id}");
            
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

    }
}

/*
 * 
 * using PPA.Models;
using PPA.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PPA.ViewModels
{
    public class TaskManagerViewModel: BaseViewModel
    {
        private TaskItem _selectedTask;

        public ObservableCollection<TaskItem> Tasks { get; }
        public Command LoadTasksCommand { get; }
        public Command AddTaskCommand  { get; }
        public Command<TaskItem> TaskTapped { get; }

        public TaskManagerViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            LoadTasksCommand = new Command(async () => await ExecuteLoadTasksCommand());
            TaskTapped = new Command<TaskItem>(OnTaskSelected);
            AddTaskCommand = new Command(OnAddTask);

        }

        async Task ExecuteLoadTasksCommand()
        {
            IsBusy = true;

            try
            {
                Tasks.Clear();
                var tasks = await DataStore.GetTasksAsync(true);
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            _selectedTask = null;
        }

        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                SetProperty(ref _selectedTask, value);
                OnTaskSelected(value);
            }
        }

        private async void OnAddTask(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewTaskPage));
        }

        async void OnTaskSelected(TaskItem task)
        {
            if(task == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TaskDetailPage)}?{nameof(TaskDetailViewModel.TaskId)}={task.Id}");
            
        }

    }
}

*/