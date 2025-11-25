/**
* @file TaskItem.cs
* @brief 
*/
using NUnit.Framework;
using TaskManagerApp.Models;
using System;


namespace TaskManagerApp.Tests
{
    [TestFixture]
    /**
    * @class TaskItemTests 
    * @brief This class includes tests that verify TaskItem functionality.
    * @property _task 
    */
    public class TaskItemTests
    {
        private TaskItem _task;

        /**
        *
        */
        [SetUp]
        public void Setup()
        {
            _task = new TaskItem("Test Task", "Test Description");
        }

        [Test]
        public void TaskItem_ShouldInitializeTitleAndDescription()
        {
            // Arrange + Act handled in SetUp
            // Assert
            Assert.That(_task.Title, Is.EqualTo("Test Task"));
            Assert.That(_task.Description, Is.EqualTo("Test Description"));
        }

        [Test]
        public void TaskItem_ShouldNotInitializeTask_WhenTitleIsNull()
        {
            Assert.Throws<ArgumentException>(() => new TaskItem(null, "Test Description"));
        }

        [Test]
        public void TaskItem_ShouldAllowInitialization_WhenDescriptionIsEmpty()
        {
            _task = new TaskItem("Test Task", "");
            Assert.That(_task.Title, Is.EqualTo("Test Task"));
            Assert.That(_task.Description, Is.Empty);
        }

        [Test]
        public void TaskItem_ShouldAllowInitialization_WhenDescriptionIsNull()
        {
            _task = new TaskItem("Test Task", null);
            Assert.That(_task.Title, Is.EqualTo("Test Task"));
            Assert.That(_task.Description, Is.Null);
        }

        [Test]
        public void TaskItem_ShouldSetStateToPending_WhenInitialized()
        {
            // Arrange + Act handled in SetUp
            // Assert
            Assert.That(_task.State, Is.EqualTo(TaskState.Pending));
        }

        [Test]
        public void MarkCompleted_ShouldSetStateToCompleted()
        {
            _task.MarkCompleted();
            Assert.That(_task.State, Is.EqualTo(TaskState.Completed));
        }

        [Test]
        public void Start_ShouldSetStateToInProgress()
        {
            _task.Start();
            Assert.That(_task.State, Is.EqualTo(TaskState.InProgress));
        }

        [Test]
        public void Reset_ShouldSetStateToPending_WhenTaskWasCompleted()
        {
            _task.MarkCompleted();

            _task.Reset();

            Assert.That(_task.State, Is.EqualTo(TaskState.Pending));
        }

        [Test]
        public void Reset_ShouldSetStateToPending_WhenTaskWasInProgress()
        {
            _task.Start();

            _task.Reset();

            Assert.That(_task.State, Is.EqualTo(TaskState.Pending));
        }
    }


}