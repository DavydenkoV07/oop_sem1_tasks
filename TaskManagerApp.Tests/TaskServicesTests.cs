/**
* @file TaskServicesTests.cs
* @brief Unit Tests for TaskService.
*/
using NUnit.Framework;
using TaskManagerApp.Models;
using TaskManagerApp.Services;
using System.Linq;
using System;

namespace TaskManagerApp.Tests
{
    
    [TestFixture]
    /** 
    * @class TaskServicesTests
    * @brief A set of tests that verify TaskService functionality.
    * 
    */
    public class TaskServicesTests
    {
        private TaskService service;

        [SetUp]
        /**
        * @brief initialize new TaskService before each test
        */
        public void Setup()
        {
            service = new TaskService();
        }

        [Test]
        /**
        * @brief Test checks if a new TaskItem is added to the service
        * @test 
        *      
        * @see TaskService.AddTask(TaskItem)
        */
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
        /**
        * @brief Checks if AddTask throws an exception when TaskItem is null
        * @test 
        * - calling AddTask with null argument
        * - ArgumentNullException is thrown
        *
        * @exception ArgumentNullException thrown when TaskItem is null
        * @see TaskService.AddTask(TaskItem)
        */
        public void AddTask_ShouldThrow_WhenTaskIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => service.AddTask(null));
        }

        [Test]
        /**
        * @brief Checks if AddTask throws an exception when TaskItem title is empty string
        * @test Steps:
        * - Initialize new TaskItem with empty title
        * - Call AddTask  
        * - ArgumentException is thrown
        * @exception ArgumentException thrown if TaskItem title is empty string
        * @see TaskService.AddTask(TaskItem)
        */
        public void AddTask_ShouldThrow_WhenEmptyTitle()
        {
            Assert.Throws<ArgumentException>(() => service.AddTask(new TaskItem("", "Test Description")));
        }
        
        [Test]

        public void AddTask_ShouldAllowToAdd_WhenEmptyDescription()
        {
            // Arrange
            var task = new TaskItem("Test Title", "");
            //Act
            service.AddTask(task);
            //Assert
            var tasks = service.GetAllTasks();
            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(tasks.First().Description, Is.Empty);
        }
        
        [Test]
        public void AddTask_ShouldAllowToAdd_WhenNullDescription()
        {
            // Arrange
            var task = new TaskItem("Test Title", null);
            //Act
            service.AddTask(task);
            //Assert
            var tasks = service.GetAllTasks();
            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(tasks.First().Description, Is.Null);
        }

        [Test]
        public void RemoveTask_ShouldRemoveTaskFromListAndReturnTrue_WhenTaskIsFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            var isTaskRemoved = service.RemoveTask("Test Title");
            //Assert
            var tasks = service.GetAllTasks();
            Assert.That(tasks.Count(), Is.EqualTo(0));
            Assert.That(isTaskRemoved, Is.True);
        }

        [Test]
        public void RemoveTask_ShouldThrow_WhenTaskTitleIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => service.RemoveTask(null));
        }

        [Test]
        public void RemoveTask_ShouldRemoveOnlyFirstMatchingTask_WhenDuplicatesExist()
        {
            // Arrange
            var task1 = new TaskItem("Test Title", "Test Description 1");
            var task2 = new TaskItem("Test Title", "Test Description 2");
            service.AddTask(task1);
            service.AddTask(task2);
            //Act
            var isTaskRemoved = service.RemoveTask("Test Title");
            //Assert
            var tasks = service.GetAllTasks();
            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(isTaskRemoved, Is.True);
        }

        [Test]
        public void RemoveTask_ShouldReturnFalse_WhenTaskIsNotFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            var isTaskRemoved = service.RemoveTask("Test Title Failed Remove");
            //Assert
            var tasks = service.GetAllTasks();
            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(isTaskRemoved, Is.False);
        }

        [Test]
        public void GetByState_ShouldReturnTaskInProgress_WhenTaskIsFound()
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
            Assert.That(tasks.First().Title, Is.EqualTo("Test Title"));
        }

        [Test]
        public void GetByState_ShouldReturnTaskCompleted_WhenTaskIsFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            task.MarkCompleted();
            service.AddTask(task);
            //Act
            var tasks = service.GetByState(TaskState.Completed);
            //Assert
            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(tasks.First().State, Is.EqualTo(TaskState.Completed));
            Assert.That(tasks.First().Title, Is.EqualTo("Test Title"));
        }

        [Test]
        public void GetByState_ShouldReturnTaskPending_WhenTaskIsFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            var tasks = service.GetByState(TaskState.Pending);
            //Assert
            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(tasks.First().State, Is.EqualTo(TaskState.Pending));
            Assert.That(tasks.First().Title, Is.EqualTo("Test Title"));
        }

        [Test]
        public void GetByState_ShouldReturnEmptyList_WhenTaskIsNotFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            var tasks = service.GetByState(TaskState.InProgress);
            //Assert
            Assert.That(tasks, Is.Empty);
        }

    
        [Test]
        public void FindTask_ShouldReturnTask_WhenTaskIsFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            var taskByTitle = service.FindTask("Test Title");
            //Assert
            Assert.That(taskByTitle, Is.Not.Null);
            Assert.That(taskByTitle.Title, Is.EqualTo("Test Title"));
        }

        [Test]
        public void FindTask_ShouldThrow_WhenTaskTitleIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => service.FindTask(null));
        }

        [Test]
        public void FindTask_ShouldReturnOnlyFirstTask_WhenDuplicateTitlesExist()
        {
            // Arrange
            var task1 = new TaskItem("Test Title", "Test Description 1");
            var task2 = new TaskItem("Test Title", "Test Description 2");
            service.AddTask(task1);
            service.AddTask(task2);
            //Act
            var taskByTitle = service.FindTask("Test Title");
            //Assert
            Assert.That(taskByTitle, Is.Not.Null);
            Assert.That(taskByTitle.Title, Is.EqualTo("Test Title"));
            Assert.That(taskByTitle.Description, Is.EqualTo("Test Description 1"));
        }

        [Test]
        public void FindTask_ShouldReturnNull_WhenTaskIsNotFound()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            var taskByTitle = service.FindTask("Test Title Failed");
            //Assert
            Assert.That(taskByTitle, Is.Null);

        }

        [Test]
        public void GetCompletionRate_ShouldReturnCompletionRate()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            task.MarkCompleted();
            service.AddTask(task);
            //Act
            var completionRate = service.GetCompletionRate();
            //Assert
            Assert.That(completionRate, Is.EqualTo(1).Within(0.001));
        }

        [Test]
        public void GetCompletionRate_ShouldReturnZeroRate()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            var completionRate = service.GetCompletionRate();
            //Assert
            Assert.That(completionRate, Is.EqualTo(0).Within(0.001));
        }

        [Test]
        public void GetCompletionRate_ShouldReturnZeroRate_WhenNoTasks()
        {
            // Arrange
            
            //Act
            var completionRate = service.GetCompletionRate();
            //Assert
            Assert.That(completionRate, Is.EqualTo(0).Within(0.001));
        }

        [Test]
        public void GetCompletionRate_ShouldReturnPartialRate()
        {
            // Arrange
            var completedTask = new TaskItem("Completed Task", "Done");
            completedTask.MarkCompleted();
            var pendingTask = new TaskItem("Pending Task", "Not done");
            service.AddTask(completedTask);
            service.AddTask(pendingTask);
            // Act
            var completionRate = service.GetCompletionRate();
            // Assert
            Assert.That(completionRate, Is.EqualTo(0.5).Within(0.001));
        }


        [Test]
        public void ClearAll_ShouldRemoveAllTasks()
        {
            // Arrange
            var task = new TaskItem("Test Title", "Test Description");
            service.AddTask(task);
            //Act
            service.ClearAll();
            //Assert
            Assert.That(service.GetAllTasks(), Is.Empty);
        }

        [TearDown]
        public void ClearService()
        {
            service.ClearAll();
        }


    }
}