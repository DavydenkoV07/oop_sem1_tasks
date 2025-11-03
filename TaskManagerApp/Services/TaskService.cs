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

        /// <summary>
        /// Додати нове завдання
        /// </summary>
        public void AddTask(TaskItem task)
        {
            //fixed after testing
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");
            _tasks.Add(task);
        }

        /// <summary>
        /// Отримати всі завдання
        /// </summary>
        public IEnumerable<TaskItem> GetAllTasks() => _tasks;

        /// <summary>
        /// Отримати завдання за певним станом (Pending, InProgress, Completed)
        /// </summary>
        public IEnumerable<TaskItem> GetByState(TaskState state)
        {
            return _tasks.Where(t => t.State == state);
        }

        /// <summary>
        /// Видалити завдання за назвою
        /// </summary>
        public bool RemoveTask(string title)
        {
            //fixed after testing
            if (title == null)
                throw new ArgumentNullException(nameof(title), "Title cannot be null.");

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
             //fixed after testing
            if (title == null)
                throw new ArgumentNullException(nameof(title), "Title cannot be null.");

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