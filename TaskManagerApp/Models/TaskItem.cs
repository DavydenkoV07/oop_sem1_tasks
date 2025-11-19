/**
* @file TaskItem.cs
* @brief 
*/

namespace TaskManagerApp.Models
{
   
   
    /**
    * @enum TaskState
    * @brief Defines possible task lifecycle states. (Pending - waiting, InProgress - in progress, Completed - completed)
    */
    public enum TaskState
    {
        Pending,
        InProgress,
        Completed
    }

    
    
    /**
    * @class TaskItem
    * @brief This class allows you to create a task, stores data about it (title, description) 
    * and allows you to manage the task by changing its state.
    * @property Title Task name. Cannot be empty or null. 
    * @property Description Task description. Can be empty or null.
    */
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }

        /**
        * @brief Initializes the task.
        * @details This method assigns the task the specified title, description, and puts it in the pending state.
        * @param title Task name.
        * @param description Short description for task.
        * @exception ArgumentException thrown when title is empty or null.
        */
        public TaskItem(string title, string description)
        {
            //fixed after testing
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            Title = title;
            Description = description;
            State = TaskState.Pending;
        }

        
        
       /**
       * @brief Changes TaskState to Completed.
       */
        public void MarkCompleted()
        {
            State = TaskState.Completed;
        }

        
        
        /**
        * @brief Changes TaskState to InProgress.
        */
        public void Start()
        {
            State = TaskState.InProgress;
        }

        
        
        /**
        * @brief Changes TaskState to Pending.
        */
        public void Reset()
        {
            State = TaskState.Pending;
        }
    }
}