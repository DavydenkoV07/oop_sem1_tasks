/**
* @file TaskService.cs
* @brief Contains the TaskService class, which inherits from BaseTaskService and adds business logic functionality.
* @namespace TaskManagerApp.Services
*/
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerApp.Models;

namespace TaskManagerApp.Services
{
    /**
    * @class TaskService
    * @brief Manages a list of tasks, providing filtering and statistics.
    */
    public class TaskService : BaseTaskService
    {
        /// @private
        /// @var _assignedUsers Dictionary for storing assigned users (key: task Title).
        private readonly Dictionary<string, User> _assignedUsers = new Dictionary<string, User>();
        
        /**
         * @brief TaskService constructor.
         */
        public TaskService() { }

        /**
         * @brief Implementation of task validation. Ensures no duplicate title.
         * @param task Task to validate.
         * @exception InvalidOperationException Thrown if a task with the same title already exists.
         */
        protected override void ValidateTask(TaskItem task)
        {
            if (_tasks.Any(t => t.Title.Equals(task.Title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"A task with title '{task.Title}' already exists.");
            }
        }

        /**
        * @brief Returns a list of tasks with a specified state.
        * @param state Task State to filter by.
        * @return IEnumerable<TaskItem> collection containing tasks that have the specified state.
        */
        public IEnumerable<TaskItem> GetByState(TaskState state)
        {
            return _tasks.Where(t => t.State == state);
        }

        /**
        * @brief Removes a task from the list by title.
        * @param title Title of task that needs to be removed.
        * @exception ArgumentNullException Thrown when task title is null.
        * @return True if the task was found and successfully removed, otherwise returns false.
        */
        public bool RemoveTask(string title)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title), "Title cannot be null.");

            var task = _tasks.FirstOrDefault(t => t.Title == title);
            if (task == null) return false;
            
            _tasks.Remove(task);
            if (_assignedUsers.ContainsKey(title)) _assignedUsers.Remove(title);
            Console.WriteLine($"Task '{title}' removed.");
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
            if (title == null)
                throw new ArgumentNullException(nameof(title), "Title cannot be null.");

            return _tasks.FirstOrDefault(t => t.Title == title);
        }

        /**
        * @brief Calculates the percentage of tasks completed in the list.
        * @return Percentage of tasks completed as a decimal (from 0.0 to 1.0).
        */
        public double GetCompletionRate()
        {
            if (_tasks.Count == 0) return 0.0;
            double completed = _tasks.Count(t => t.State == TaskState.Completed);
            return completed / _tasks.Count;
        }

        /**
         * @brief Assigns a user to a task by title.
         * @param taskTitle The title of the task.
         * @param user The user to assign.
         * @return True if assignment was successful, false if task was not found.
         */
        public bool AssignUser(string taskTitle, User user)
        {
            if (FindTask(taskTitle) == null) return false;

            _assignedUsers[taskTitle] = user;
            Console.WriteLine($"User '{user.Username}' assigned to task '{taskTitle}'.");
            return true;
        }
        
        /**
         * @brief Gets the assigned user for a task.
         * @param taskTitle The title of the task.
         * @return User object or null if no user is assigned.
         */
        public User? GetAssignedUser(string taskTitle)
        {
            if (_assignedUsers.TryGetValue(taskTitle, out User user))
            {
                return user;
            }
            return null;
        }
        
        /**
         * @brief Converts all tasks to a summary string.
         * @return A string summary of all tasks.
         */
        public string GetSummary()
        {
            return $"Total Tasks: {_tasks.Count}. Completed: {GetByState(TaskState.Completed).Count()}. Rate: {GetCompletionRate():P2}";
        }
    }
}