using System;
using System.Collections.Generic;
using TaskManagerApp.Models;

namespace TaskManagerApp.Services
{
    /**
    * @class BaseTaskService
    * @brief Provides core functionality for managing a list of tasks.
    */
    public abstract class BaseTaskService
    {
        protected readonly List<TaskItem> _tasks = new List<TaskItem>();

        /**
         * @brief Abstract method to validate a task before adding.
         * @param task Task to validate.
         */
        protected abstract void ValidateTask(TaskItem task); 

        /**
        * @brief Adds a new task to the list after validation.
        * @param task Task to add.
        * @exception ArgumentNullException Thrown when argument is null.
        * @exception InvalidOperationException Thrown if validation fails.
        */
        public void AddTask(TaskItem task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");
            
            ValidateTask(task);
            _tasks.Add(task);
            Console.WriteLine($"Task '{task.Title}' added to service.");
        }

        /**
        * @brief Returns a complete list of all current tasks.
        * @return IEnumerable<TaskItem> collection containing all tasks.
        */
        public IEnumerable<TaskItem> GetAllTasks() => _tasks;

        /**
        * @brief Deletes all tasks from the internal list.
        */
        public void ClearAll()
        {
            _tasks.Clear();
            Console.WriteLine("All tasks cleared.");
        }
    }
}