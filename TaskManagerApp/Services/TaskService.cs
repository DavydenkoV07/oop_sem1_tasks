
/**
* @file TaskItem.cs
* @brief Contains the TaskItem model class and the associated TaskState list.
* This file defines the data structure used to represent
* an individual task in the system.
*/
using System.Collections.Generic;
using System.Linq;
using TaskManagerApp.Models;

namespace TaskManagerApp.Services
{
    
    /**
    * @class TaskService
    * @brief This class creates list of tasks, stores them and allows to manage them. 
    * 
    */
    public class TaskService
    {
        private readonly List<TaskItem> _tasks;

        /**
        * @brief Initializes list of tasks
        *  
        */
        public TaskService()
        {
            _tasks = new List<TaskItem>();
        }

        /**
        * @brief This method adds new task to the list.
        * @details 
        * @param task Task that you want to add. 
        * @exception ArgumentNullException Thrown when argument is null. 
        */
        public void AddTask(TaskItem task)
        {
            //fixed after testing
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");
            _tasks.Add(task);
        }

        /**
        * @brief Returns a complete list of all current tasks.
        * * @return IEnumerable<TaskItem> collection containing all tasks
        * currently stored in the service.
        */
        public IEnumerable<TaskItem> GetAllTasks() => _tasks;

        /**
        * @brief Returns a list of tasks with a specified state.
        * * This method filters the internal list of tasks (_tasks) and
        * returns only those items that match the specified state.
        * * @param state Task State (TaskState: Pending, InProgress or Completed)
        * on which to filter.
        * @return IEnumerable<TaskItem> collection containing tasks that have the specified state.
        */
        public IEnumerable<TaskItem> GetByState(TaskState state)
        {
            return _tasks.Where(t => t.State == state);
        }

        /**
        * @brief This method removes task from list.
        * @details The method searches for the first task with a matching title and removes it.
        * If the title is null, an exception is thrown. If no task is found, it returns false.
        * @param title Title of task that needs to be removed.
        * @exception ArgumentNullException Thrown when task title is null.
        * @return True if the task was found and successfully removed, otherwise returns false.
        */
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
        
        /**
        * @brief Finds the first task with the specified name.
        * @param title The name of the task to be found.
        * @return TaskItem if the task is found, otherwise returns null.
        * @throws ArgumentNullException Thrown if task title is null.
        */
        public TaskItem? FindTask(string title)
        {
             //fixed after testing
            if (title == null)
                throw new ArgumentNullException(nameof(title), "Title cannot be null.");

            return _tasks.FirstOrDefault(t => t.Title == title);
        }

        /**
        * @brief Calculates the percentage of tasks completed in the list.
        * @details The method counts the number of tasks in the TaskState.Completed state
        * and divides it by the total number of tasks.
        * @return Percentage of tasks completed as a decimal (from 0.0 to 1.0).
        * Returns 0.0 if the overall task list is empty.
        */
        public double GetCompletionRate()
        {
            if (_tasks.Count == 0) return 0;
            double completed = _tasks.Count(t => t.State == TaskState.Completed);
            return completed / _tasks.Count;
        }

        /**
        * @brief Deletes all tasks from the internal list.
        * @details After executing this method, the _tasks list will be empty.
        */
        public void ClearAll()
        {
            _tasks.Clear();
        }
    }
}