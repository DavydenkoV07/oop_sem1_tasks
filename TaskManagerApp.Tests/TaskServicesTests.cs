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
        * @test Test scenario:
        * 1. A new TaskItem object is created.
        * 2. The AddTask method is called.
        * 3. It is checked that the number of items in the list is 1
        * and that the task name matches.
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
        * @brief Test checks if AddTask throws an exception when TaskItem title is empty string
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
        
        /**
        * @brief Test Checks whether the system allows adding a task with an empty description.
        * @details Method confirms that the Description field can be an empty string
        * when creating a TaskItem and that the service (TaskService) correctly accepts and stores such a task.
        * @see TaskService::AddTask(TaskItem)
        * @see TaskItem::TaskItem(string, string)
        */
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
        /**
        * @brief Test: checks if the system allows adding tasks with a 'null' value in the description.
        * @details The method confirms that the Description field can be null
        * when creating a TaskItem and that the service (TaskService) correctly accepts and stores such a task.
        * @see TaskService::AddTask(TaskItem)
        * @see TaskItem::TaskItem(string, string)
        */
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

        /**
        * @brief Test: verifies successful removal of an existing task from the list.
        * @details The method verifies that the RemoveTask() function correctly removes a task
        * by its name, reducing the total number of tasks in the list and returning true.
        * @see TaskService::RemoveTask(string)
        */
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

        /**
        * @brief Test: Checks that the RemoveTask() method throws an ArgumentNullException
        * if the task title is null.
        * @throws ArgumentNullException The RemoveTask() method is expected to throw this exception when passed null.
        * @see TaskService::RemoveTask(string)
        */
        [Test]
        public void RemoveTask_ShouldThrow_WhenTaskTitleIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => service.RemoveTask(null));
        }

        /**
        * @brief Test: Checks that RemoveTask() only removes the first task found,
        * if there are duplicate names.
        * @see TaskService::RemoveTask(string)
        */
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
        /**
        * @brief Test: Verifies that RemoveTask() returns false when a task is not found.
        * @see TaskService::RemoveTask(string)
        */
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

        /**
        * @brief Test: verifies that GetByState() returns a task with a state InProgress.
        * @see TaskService::GetByState(TaskState)
        */
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

        /**
        * @brief Test: verifies that GetByState() returns a task with a state Completed.
        * @see TaskService::GetByState(TaskState)
        */
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

        /**
        * @brief Test: verifies that GetByState() returns a task with a state Pending.
        * @see TaskService::GetByState(TaskState)
        */
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

        /**
        * @brief Test: Checks that GetByState() returns an empty list when task is not found.
        * @see TaskService::GetByState(TaskState)
        */
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

        /**
        * @brief Test: Verifies that the FindTask() method finds and returns a task by its name.
        * @see TaskService::FindTask(string)
        */
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

        /**
        * @brief Test: Checks that the FindTask() method throws an ArgumentNullException if the task title is null.
        * @throws ArgumentNullException The FindTask() method is expected to throw this exception when passed null.
        * @see TaskService::FindTask(string)
        */
        [Test]
        public void FindTask_ShouldThrow_WhenTaskTitleIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => service.FindTask(null));
        }

        /**
        * @brief Test: Checks that FindTask() returns only the first element when there are tasks with the same name in the list.
        * @see TaskService::FindTask(string)
        */
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

        /**
        * @brief Test: Checks that FindTask() returns null when a task is not found.
        * @see TaskService::FindTask(string)
        */
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

        /**
        * @brief Test: Checks that GetCompletionRate() correctly calculates 100% completion percentage.
        * @see TaskService::GetCompletionRate()
        */
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

        /**
        * @brief Test: Checks that GetCompletionRate() correctly calculates 0% completion percentage.
        * @see TaskService::GetCompletionRate()
        */
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

        /**
        * @brief Test: Checks that GetCompletionRate() correctly calculates 0% completion percentage when list of tasks is empty.
        * @see TaskService::GetCompletionRate()
        */
        [Test]
        public void GetCompletionRate_ShouldReturnZeroRate_WhenNoTasks()
        {
            // Arrange
            
            //Act
            var completionRate = service.GetCompletionRate();
            //Assert
            Assert.That(completionRate, Is.EqualTo(0).Within(0.001));
        }

        /**
        * @brief Test: Checks that GetCompletionRate() correctly calculates 50% completion percentage.
        * * @see TaskService::GetCompletionRate()
        */
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

        /**
        * @brief Test: Checks that the ClearAll() method removes all tasks from the list.
        * @see TaskService::ClearAll()
        * @see TaskService::GetAllTasks()
        */
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

        /**
        * @brief Removes all tasks from the service after each test is completed.
        */
        [TearDown]
        public void ClearService()
        {
            service.ClearAll();
        }


    }
}