using PPA.Services;
using PPA.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly:Dependency(typeof(TaskService))]
namespace PPA.Services
{
    public class TaskService: ITaskDataStore
    {
        SQLiteAsyncConnection db;

        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "TaskData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<TaskItem>();
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await Init();
            var id = await db.InsertAsync(task);
        }

        /* do later 
        public async Task UpdateTaskAsync(TaskItem task)
        {
           
        }
        */

        public async Task DeleteTaskAsync(int id)
        {
            await Init();
            await db.DeleteAsync<TaskItem>(id);
        }

        public async Task<IEnumerable<TaskItem>> GetTasksAsync()
        {
            await Init();

            var tasks = await db.Table<TaskItem>().ToListAsync();
            return tasks;
        }

        public async Task<TaskItem> GetTaskAsync(int id)
        {
            await Init();
            var task = await db.Table<TaskItem>().FirstOrDefaultAsync(c => c.Id == id);

            return task;
        }
    }
}

/*
 * 
 * OLD DB STUFF
using PPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskItem = PPA.Models.TaskItem;

namespace PPA.Services
{
    public class TaskService
    {
    readonly List<TaskItem> tasks;

        public TaskService()
        {
            tasks = new List<TaskItem>()
            {
                new TaskItem { Id = Guid.NewGuid().ToString(), Name = "First item", Description="This is an item description." },
                new TaskItem { Id = Guid.NewGuid().ToString(), Name = "Second item", Description="This is an item description." },
            };
        }

        public async Task<bool> AddTaskAsync(TaskItem task)
    {
            tasks.Add(task);
            return await Task.FromResult(true);
    }

    public async Task<bool> UpdateTaskAsync(TaskItem task)
        {
            var oldTask = tasks.Where((TaskItem arg) => arg.Id == task.Id).FirstOrDefault();
            tasks.Remove(oldTask);
            tasks.Add(task);

            return await Task.FromResult(true);
        }

    public async Task<bool> DeleteTaskAsync(string id)
        {
            var oldTask = tasks.Where((TaskItem arg) => arg.Id == id).FirstOrDefault();
            tasks.Remove(oldTask);

            return await Task.FromResult(true);
        }

    public async Task<TaskItem> GetTaskAsync(string id)
        {
            return await Task.FromResult(tasks.FirstOrDefault(s => s.Id == id));
        }

    public async Task<IEnumerable<TaskItem>> GetTasksAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(tasks);
        }
    }
}

*/
