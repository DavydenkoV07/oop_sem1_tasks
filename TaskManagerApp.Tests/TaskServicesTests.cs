using NUnit.Framework;
using TaskManagerApp.Models;
using TaskManagerApp.Services;

namespace TaskManagerApp.Tests
{
    [TestFixture]
    public class TaskServicesTests
    {
        private TaskService service;

        [SetUp]
        public void Setup()
        {
            service = new TaskService();
        }

        [Test]
        public void AddTask_ShouldAddNewTaskToList()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            //Act
            service.AddTask(task);
            //Assert
            var tasks = service.GetAllTasks();
            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(tasks.First().Title, Is.EqualTo("Test Title"));

        }
        [Test]
        public void RemoveTask_ShouldRemoveTaskFromListAndReturnTrue_WhenTaskIsFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            var IsTaskRemoved = service.RemoveTask("Test Title");
            //Assert
            var tasks = service.GetAllTasks();
            Assert.That(tasks.Count(), Is.EqualTo(0));
            Assert.That(IsTaskRemoved, Is.True);
        }

        [Test]
        public void RemoveTask_ShouldReturnFalse_WhenTaskIsNotFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            var IsTaskRemoved = service.RemoveTask("Test Title Failed Remove");
            //Assert
            var tasks = service.GetAllTasks();
            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(IsTaskRemoved, Is.False);
        }

        [Test]
        public void GetByState_ShouldReturnTaskByState_WhenTaskIsFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            task.Start();
            service.AddTask(task);
            //Act
            var tasks = service.GetByState(TaskState.InProgress);
             //Assert
            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(tasks.First().State, Is.EqualTo(TaskState.InProgress));
        }


    }
}