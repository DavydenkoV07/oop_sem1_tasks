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
        Pending,
        InProgress,
        Completed,
        OnHold 
    }
    
    /**
     * @enum PriorityLevel
     * @brief Defines the level of priority for a task.
     */
    public enum PriorityLevel
    {
        Low,
        Medium,
        High,
        Critical
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

        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
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