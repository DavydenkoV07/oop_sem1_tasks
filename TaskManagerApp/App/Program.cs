/**
* @file Program.cs
* @brief Contains the AppManager class for coordinating program operation and the Program class (entry point).
* @namespace TaskManagerApp.App
*/
using System;
using System.Collections.Generic;
using System.Linq; // Потрібен для методу OfType<T>()
using TaskManagerApp.Models;
using TaskManagerApp.Services;

namespace TaskManagerApp.App
{
    /**
     * @class AppManager
     * @brief Manages the application workflow and coordinates services.
     */
    public class AppManager 
    {
        // @private
        /// @var _taskService The instance of the main task service.
        private readonly TaskService _taskService = new TaskService();
        /// @private
        /// @var _reportGenerator The instance of the report generator.
        private readonly ReportGenerator _reportGenerator = new ReportGenerator("Daily Task Summary");
        
        /**
         * @brief Runs the main application demonstration logic.
         */
        public void RunDemo()
        {
            Console.WriteLine("=== Task Manager Application Demo ===");
            
            // 1. Creating different types of tasks
            var simpleTask = new TaskItem("Buy groceries", "Milk, bread, and eggs.");
            var deadlineTask = new TimedTask("Project Alpha", "Finish main module.", DateTime.Now.AddDays(-1));
            var highPriorityTask = new PriorityTask("Fix critical bug", "Database connection issue.", PriorityLevel.Critical);
            
            // 2. Adding tasks via the polymorphic interface (BaseTaskService.AddTask)
            _taskService.AddTask(simpleTask);
            _taskService.AddTask(deadlineTask); // Overdue task
            _taskService.AddTask(highPriorityTask);
            
            // 3. Changing state and additional actions
            simpleTask.Start(); 
            highPriorityTask.IncreasePriority(); 
            
            // 4. Assigning a user
            var user1 = new User("Alice", "alice@example.com");
            _taskService.AssignUser(simpleTask.Title, user1); 

            Console.WriteLine("\n--- Task State Management ---");
            Console.WriteLine($"Is '{deadlineTask.Title}' overdue? {deadlineTask.IsOverdue()}"); 

            // 5. Demonstrating Dynamic Polymorphism (via ITaskReporter interface)
            Console.WriteLine("\n--- Dynamic Polymorphism (ITaskReporter) Report ---");
            var tasksForReport = new List<ITaskReporter> { simpleTask, deadlineTask, highPriorityTask };
            string report = _reportGenerator.GenerateConsolidatedReport(tasksForReport); 
            Console.WriteLine(report);

            // 6. Demonstrating Static Polymorphism (Generics)
            Console.WriteLine("\n--- Static Polymorphism (TaskSearcher<T>) ---");
            var allTasks = _taskService.GetAllTasks();
            var searchResults = TaskSearcher<TaskItem>.SearchByKeyword(allTasks, "bug"); 
            Console.WriteLine($"Found {searchResults.Count} task(s) with keyword 'bug'.");
            
            var timedTasks = allTasks.OfType<TimedTask>();
            int overdueCount = TaskSearcher<TimedTask>.CountOverdue(timedTasks); 
            Console.WriteLine($"Overdue Timed Tasks: {overdueCount}");
            
            // 7. Outputting final statistics
            Console.WriteLine("\n--- Final Summary ---");
            Console.WriteLine(_taskService.GetSummary()); 
        }
    }
    
    /**
     * @class Program
     * @brief The program entry point class.
     */
    public class Program 
    {
        /**
         * @brief The main method that starts the application.
         * @param args Command line arguments (not used).
         */
        public static void Main(string[] args)
        {
            try
            {
                new AppManager().RunDemo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}