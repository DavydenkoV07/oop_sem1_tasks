namespace TaskManagerApp.Models
{
   
    /// Статус завдання (Pending — очікує, InProgress — у процесі, Completed — виконано)
    
    public enum TaskState
    {
        Pending,
        InProgress,
        Completed
    }

    
    /// Модель завдання для менеджера задач
    
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }

        public TaskItem(string title, string description)
        {
            //fixed after testing
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            Title = title;
            Description = description;
            State = TaskState.Pending;
        }

        
        /// Позначити завдання як виконане
       
        public void MarkCompleted()
        {
            State = TaskState.Completed;
        }

        
        /// Почати виконання завдання
        
        public void Start()
        {
            State = TaskState.InProgress;
        }

        
        /// Скинути стан завдання у “очікує”
        
        public void Reset()
        {
            State = TaskState.Pending;
        }
    }
}