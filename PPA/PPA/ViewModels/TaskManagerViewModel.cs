using PPA.Models;
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
