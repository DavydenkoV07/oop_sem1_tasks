using System;

namespace TaskManagerApp.Models
{
    /**
    * @class TimedTask
    * @brief Represents a task with a defined deadline.
    */
    public class TimedTask : TaskItem
    {
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
         */
        public override string GetTaskReport()
        {
            string baseReport = base.GetTaskReport();
            return baseReport + $"\nDue Date: {DueDate.ToShortDateString()} {(IsOverdue() ? "(OVERDUE)" : "")}";
        }
    }
}