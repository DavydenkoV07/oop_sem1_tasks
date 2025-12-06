/**
* @file TaskItem.cs
* @brief Contains the base task model TaskItem, the enumerations TaskState, PriorityLevel, and the ITaskReporter interface.
* @namespace TaskManagerApp.Models
*/
using System;

namespace TaskManagerApp.Models
{
    // === Enumerations ===
    
    /**
    * @enum TaskState
    * @brief Defines possible task lifecycle states.
    */
    public enum TaskState
    {
        Pending,///< The task is waiting to be started.
        InProgress, ///< The task is currently being worked on.
        Completed,  ///< The task is finished.
        OnHold      ///< The task is temporarily paused.
    }
    
    /**
     * @enum PriorityLevel
     * @brief Defines the level of priority for a task.
     */
    public enum PriorityLevel
    {
        Low,///< Low priority.
        Medium,   ///< Medium priority.
        High,     ///< High priority.
        Critical  ///< Critical priority.
    }

    // === Interface for Polymorphism 2 (Dynamic) ===
    
    /**
    * @interface ITaskReporter
    * @brief Interface for objects that can generate a formatted task report.
    */
    public interface ITaskReporter
    {
        /**
         * @brief Generates a formatted string report for the task.
         * @return Formatted string containing task details.
         */
        string GetTaskReport();
    }

    // === Class 1: TaskItem (Base Class of Hierarchy 1) ===

    /**
    * @class TaskItem
    * @brief Base class for a task, storing core data and state management.
    */
    public class TaskItem : ITaskReporter
    {
        private DateTime _creationDate;

        /// @property Title The name of the task.
        public string Title { get; set; }
        /// @property Description The description of the task.
        public string Description { get; set; }
        /// @property State The completion state of the task.
        public TaskState State { get; set; }
        /// @property CreationDate The date and time the task was created.
        public DateTime CreationDate => _creationDate;

        /**
        * @brief Initializes the task.
        * @param title Task name.
        * @param description Short description for task.
        * @exception ArgumentException thrown when title is empty or null.
        */
        public TaskItem(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            
            Title = title;
            Description = description;
            State = TaskState.Pending;
            _creationDate = DateTime.Now; 
        }

        /**
        * @brief Changes TaskState to Completed.
        */
        public void MarkCompleted()
        {
            State = TaskState.Completed;
            Console.WriteLine($"Task '{Title}' marked as completed.");
        }

        /**
        * @brief Changes TaskState to InProgress.
        */
        public void Start()
        {
            if (State == TaskState.Pending || State == TaskState.OnHold)
            {
                State = TaskState.InProgress;
                Console.WriteLine($"Task '{Title}' started.");
            }
        }

        /**
        * @brief Changes TaskState to Pending.
        */
        public void Reset()
        {
            State = TaskState.Pending;
            Console.WriteLine($"Task '{Title}' reset to pending.");
        }

        /**
         * @brief Changes TaskState to OnHold.
         */
        public void PutOnHold()
        {
             if (State == TaskState.InProgress)
             {
                 State = TaskState.OnHold;
                 Console.WriteLine($"Task '{Title}' put On Hold.");
             }
        }

        /**
         * @brief Calculates task age in days.
         * @return Task age in days.
         */
        public double CalculateAgeInDays()
        {
            return (DateTime.Now - _creationDate).TotalDays;
        }

        /**
         * @brief Generates a formatted string report for the task.
         * @return Formatted string containing task details.
         */
        public virtual string GetTaskReport()
        {
            return $"--- Task: {Title} ---\nState: {State}\nCreated: {CreationDate.ToShortDateString()}\nDescription: {Description}";
        }
    }
}