using System;

namespace TaskManagerApp.Models
{
    /**
    * @class PriorityTask
    * @brief Represents a task with a specific priority level.
    */
    public class PriorityTask : TaskItem
    {
        public PriorityLevel Priority { get; set; }

        /**
        * @brief Initializes the priority task.
        * @param title Task name.
        * @param description Short description.
        * @param priority Level of priority.
        */
        public PriorityTask(string title, string description, PriorityLevel priority)
            : base(title, description)
        {
            Priority = priority;
        }

        /**
         * @brief Increases the task priority level (if not already Critical).
         */
        public void IncreasePriority()
        {
            if (Priority < PriorityLevel.Critical)
            {
                Priority++;
                Console.WriteLine($"Priority for '{Title}' increased to {Priority}.");
            }
        }

        /**
         * @brief Overrides base method to include priority in the report.
         */
        public override string GetTaskReport()
        {
            string baseReport = base.GetTaskReport();
            return baseReport + $"\nPriority: {Priority}";
        }
    }
}