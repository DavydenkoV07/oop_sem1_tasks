using System;
using TaskManagerApp.Models;
using TaskManagerApp.Services;

namespace TaskManagerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Task Manager App ===\n");

            // Створюємо сервіс
            var service = new TaskService();

            // Додаємо кілька задач
            service.AddTask(new TaskItem("Buy milk", "Buy 2 liters of milk"));
            service.AddTask(new TaskItem("Do homework", "Math and physics exercises"));
            service.AddTask(new TaskItem("Clean room", "Vacuum and dust"));

            // Почнемо виконання деяких
            var homework = service.FindTask("Do homework");
            homework?.Start();

            var milk = service.FindTask("Buy milk");
            milk?.MarkCompleted();

            // Виведемо всі задачі
            Console.WriteLine("All tasks:");
            foreach (var task in service.GetAllTasks())
            {
                Console.WriteLine($"- {task.Title} [{task.State}]");
            }

            // Виведемо лише виконані
            Console.WriteLine("\nCompleted tasks:");
            foreach (var task in service.GetByState(TaskState.Completed))
            {
                Console.WriteLine($"- {task.Title}");
            }

            // Показати рівень виконання
            Console.WriteLine($"\nCompletion rate: {service.GetCompletionRate():P0}");

            // Видалимо задачу
            service.RemoveTask("Clean room");
            Console.WriteLine("\nAfter removing 'Clean room':");
            foreach (var task in service.GetAllTasks())
            {
                Console.WriteLine($"- {task.Title} [{task.State}]");
            }

            // Кінець
            Console.WriteLine("\nProgram finished. Press any key to exit...");
            Console.ReadKey();
        }
    }
}