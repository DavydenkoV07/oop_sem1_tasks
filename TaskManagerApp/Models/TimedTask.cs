/**
* @file TimedTask.cs
* @brief Contains the TimedTask class, which inherits from TaskItem and adds deadline functionality.
* @namespace TaskManagerApp.Models
*/
using System;

namespace TaskManagerApp.Models
{
    /**
    * @class TimedTask
    * @brief Represents a task with a defined deadline.
    */
    public class TimedTask : TaskItem
    {
        /// @property DueDate The final execution date for the task.
        public DateTime DueDate { get; set; }

        /**
        * @brief Initializes the timed task.
        * @param title Task name.
        * @param description Short description.
        * @param dueDate The deadline for the task.
        */
        public TimedTask(string title, string description, DateTime dueDate)
            : base(title, description)
        {
            DueDate = dueDate;
        }

        /**
         * @brief Checks if the task is overdue.
         * @return True if the current date is past the DueDate.
         */
        public bool IsOverdue()
        {
            return DueDate < DateTime.Now && State != TaskState.Completed;
        }

        /**
         * @brief Overrides base method to include deadline in the report.
         * @return string The extended report, including DueDate and an OVERDUE indicator if applicable.
         */
        public override string GetTaskReport()
        {
            string baseReport = base.GetTaskReport();
            return baseReport + $"\nDue Date: {DueDate.ToShortDateString()} {(IsOverdue() ? "(OVERDUE)" : "")}";
        }
    }
}