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
    /*
     * Backend for Task Manager page
     */
    public class TaskManagerViewModel: ViewModelBase
    {

        // initialize variables
        public ObservableCollection<TaskItem> Tasks { get; }
        public ObservableCollection<TaskItem> TomorrowTasks { get; }

        // initialize commands
        public AsyncCommand LoadTasksCommand { get; } 
        public AsyncCommand AddTaskCommand  { get; }
        public AsyncCommand<TaskItem> DeleteTaskCommand { get; }
        public AsyncCommand<TaskItem> TomorrowTaskCommand { get; }

        // connect to database
        ITaskDataStore TaskService;

        /*
         * Constructor
         */
        public TaskManagerViewModel()
        {
            Title = "Task Manager";
            TomorrowTasks = new ObservableCollection<TaskItem>();
            Tasks = new ObservableCollection<TaskItem>();
            LoadTasksCommand = new AsyncCommand(LoadTasks); 
            AddTaskCommand = new AsyncCommand(OnAddTask);
            DeleteTaskCommand = new AsyncCommand<TaskItem>(OnDeleteTask);
            TomorrowTaskCommand = new AsyncCommand<TaskItem>(MoveToTomorrow);

            TaskService = DependencyService.Get<ITaskDataStore>();


        }

        /*
         * Loads TaskItems from database
         * 
         * Places loaded tasks in one of two lists
         * based on TODAY boolean value
         */
        async Task LoadTasks()
        {
            IsBusy = true;

            // clear existing lists
            Tasks.Clear();
            TomorrowTasks.Clear();

            var tasks = await TaskService.GetTasksAsync();
            foreach (var task in tasks)
            {
                // if Task for today and not currently in list, then add to Tasks list
                if (task.Today && !Tasks.Contains(task))
                {
                    Tasks.Add(task);
                }
                // else if not for today and not currently in list, then add to TomorrowTasks list
                else if (!TomorrowTasks.Contains(task))
                {

                    TomorrowTasks.Add(task);    
                }
            }

            IsBusy = false;
        } 


        /*
         * Executes when 'Add' button pushed
         * Goes to New Task Page
         */
        private async Task OnAddTask()
        {
            await Shell.Current.GoToAsync(nameof(NewTaskPage));
        }

        /*
         * Executes when 'Delete' button pushed
         * Removes TaskItem and reloads TaskItems
         */
        private async Task OnDeleteTask(TaskItem task)
        {
            await TaskService.DeleteTaskAsync(task.Id);
            await LoadTasks();
        }

        /*
         * Executes when 'Tomorrow' button pushed
         * Sets the TODAY boolean to false (Tomorrow)
         * Reloads TaskItems
         * 
         * NOTE: I know deleting and adding a new object is not ideal
         * but this is the only way I could get it to work
         */
        private async Task MoveToTomorrow(TaskItem task)
        {
            
            TaskItem newTask = new TaskItem()
            {
                Name = task.Name,
                Description = task.Description,
                Today = false,
            };

            await TaskService.AddTaskAsync(newTask);
            await OnDeleteTask(task);
            await LoadTasks();
            
        }

        /*
         * The below code is taken from an answer on this StackOverflow post:
         * https://stackoverflow.com/questions/4529019/how-to-use-the-net-timer-class-to-trigger-an-event-at-a-specific-time
         *  The idea is to check if it is midnight and if it is, then move any tasks for Tomorrow back to Today because it
         *  is a new day
         */
        bool _ran = false; //initial setting at start up
        private async void timer_Tick(object sender, EventArgs e)
        {

            if (DateTime.Now.Hour == 0 && _ran == false)
            {
                _ran = true;
                var tasks = await TaskService.GetTasksAsync();
                foreach (var task in tasks)
                {
                    if (!task.Today)
                    {
                        TaskItem newTask = new TaskItem()
                        {
                            Name = task.Name,
                            Description = task.Description,
                            Today = true,
                        };

                        await TaskService.AddTaskAsync(newTask);
                        await OnDeleteTask(task);
                    }
                }

            }

            if (DateTime.Now.Hour != 7 && _ran == true)
            {
                _ran = false;
            }

        }

        /*
         * method for when page loads
         */
        public void OnAppearing()
        {
            IsBusy = true;
        }

    }
}