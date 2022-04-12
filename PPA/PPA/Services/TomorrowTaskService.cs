using PPA.Services;
using PPA.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(TomorrowTaskService))]

namespace PPA.Services
{
    public class TomorrowTaskService: ITomorrowTaskDataStore
    {
        SQLiteAsyncConnection db;

        async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "TomorrowTaskData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<TomorrowTaskItem>();
        }

        public async Task AddTaskAsync(TomorrowTaskItem task)
        {
            await Init();
            var id = await db.InsertAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await Init();
            await db.DeleteAsync<TomorrowTaskItem>(id);
        }

        public async Task<IEnumerable<TomorrowTaskItem>> GetTasksAsync()
        {
            await Init();

            var tasks = await db.Table<TomorrowTaskItem>().ToListAsync();
            return tasks;
        }

        public async Task<TomorrowTaskItem> GetTaskAsync(int id)
        {
            await Init();
            var task = await db.Table<TomorrowTaskItem>().FirstOrDefaultAsync(c => c.Id == id);

            return task;
        }
    }
}
