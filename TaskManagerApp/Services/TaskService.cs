using System.Collections.Generic;
using System.Linq;
using TaskManagerApp.Models;

namespace TaskManagerApp.Services
{
    
    /// Клас для управління списком завдань (TaskItem)
    
    public class TaskService
    {
        private readonly List<TaskItem> _tasks;

        public TaskService()
        {
            _tasks = new List<TaskItem>();
        }

        
        /// Додати нове завдання
        
        public void AddTask(TaskItem task)
        {
            _tasks.Add(task);
        }

        
        /// Отримати всі завдання
        
        public IEnumerable<TaskItem> GetAllTasks() => _tasks;

        
        /// Отримати завдання за певним станом (Pending, InProgress, Completed)
        
        public IEnumerable<TaskItem> GetByState(TaskState state)
        {
            return _tasks.Where(t => t.State == state);
        }


        /// Видалити завдання за назвою

        public bool RemoveTask(string title)
        {
            var task = _tasks.FirstOrDefault(t => t.Title == title);
            if (task == null) return false;
            _tasks.Remove(task);
            return true;
        }
        
        /// <summary>
        /// Знайти завдання за назвою
        /// </summary>
        public TaskItem? FindTask(string title)
        {
            return _tasks.FirstOrDefault(t => t.Title == title);
        }

        /// <summary>
        /// Отримати відсоток виконаних завдань
        /// </summary>
        public double GetCompletionRate()
        {
            if (_tasks.Count == 0) return 0;
            double completed = _tasks.Count(t => t.State == TaskState.Completed);
            return completed / _tasks.Count;
        }

        /// <summary>
        /// Очистити всі завдання
        /// </summary>
        public void ClearAll()
        {
            _tasks.Clear();
        }
    }
}