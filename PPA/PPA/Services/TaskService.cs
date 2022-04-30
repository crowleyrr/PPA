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

        /*
         * Creates the database to hold TaskItem objects.
         *
         * If database already exists, then nothing is done
         */
        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "TaskData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<TaskItem>();
        }

        /*
         * Adds a new TaskItem to the database
         */
        public async Task AddTaskAsync(TaskItem task)
        {
            await Init();
            task.Id = Guid.NewGuid();
            var id = await db.InsertAsync(task);
        }

        /*
         * Deletes an existing TaskItem from the database
         */
        public async Task DeleteTaskAsync(Guid id)
        {
            await Init();
            await db.DeleteAsync<TaskItem>(id);
        }

        /*
         * Returns all existing TaskItems as a list
         */
        public async Task<IEnumerable<TaskItem>> GetTasksAsync()
        {
            await Init();

            var tasks = await db.Table<TaskItem>().ToListAsync();
            return tasks;
        }
    }
}
