/**
* @file TaskItem.cs
* @brief Contains a TaskItem model class and an associated TaskState list.
*/
using NUnit.Framework;
using TaskManagerApp.Models;
using System;


namespace TaskManagerApp.Tests
{
    /**
    * @class TaskItemTests 
    * @brief This class includes tests that verify TaskItem functionality.
    * @property _task initializes task.
    */
    [TestFixture]
    public class TaskItemTests
    {
        private TaskItem _task;

        /**
        * @brief Initializes the test TaskItem object before each test.
        */
        [SetUp]
        public void Setup()
        {
            _task = new TaskItem("Test Task", "Test Description");
        }

        /**
        * @brief Test: Checks that the TaskItem constructor correctly initializes the Title and Description properties.
        * @see Setup()
        * @see TaskItem::TaskItem(string, string)
        */
        [Test]
        public void TaskItem_ShouldInitializeTitleAndDescription()
        {
            // Arrange + Act handled in SetUp
            // Assert
            Assert.That(_task.Title, Is.EqualTo("Test Task"));
            Assert.That(_task.Description, Is.EqualTo("Test Description"));
        }

        /**
        * @brief Test: Checks that the TaskItem constructor throws an ArgumentException when the Title field is null.
        * @throws ArgumentException The constructor is expected to throw this exception when passed null.
        * @see TaskItem::TaskItem(string, string)
        */
        [Test]
        public void TaskItem_ShouldNotInitializeTask_WhenTitleIsNull()
        {
            Assert.Throws<ArgumentException>(() => new TaskItem(null, "Test Description"));
        }

        /**
        * @brief Test: Checks that the TaskItem constructor allows initialization with an empty description.
        * @see TaskItem::TaskItem(string, string)
        */
        [Test]
        public void TaskItem_ShouldAllowInitialization_WhenDescriptionIsEmpty()
        {
            _task = new TaskItem("Test Task", "");
            Assert.That(_task.Title, Is.EqualTo("Test Task"));
            Assert.That(_task.Description, Is.Empty);
        }

        /**
        * @brief Test: Checks that the TaskItem constructor allows initialization with a null description.
        * @see TaskItem::TaskItem(string, string)
        */
        [Test]
        public void TaskItem_ShouldAllowInitialization_WhenDescriptionIsNull()
        {
            _task = new TaskItem("Test Task", null);
            Assert.That(_task.Title, Is.EqualTo("Test Task"));
            Assert.That(_task.Description, Is.Null);
        }

        /**
        * @brief Test: Checks that the TaskItem constructor sets the initial task state to Pending.
        * @see TaskItem::TaskItem(string, string)
        * @see Setup()
        */
        [Test]
        public void TaskItem_ShouldSetStateToPending_WhenInitialized()
        {
            // Arrange + Act handled in SetUp
            // Assert
            Assert.That(_task.State, Is.EqualTo(TaskState.Pending));
        }

        /**
        * @brief Test: Checks that the MarkCompleted() method sets the task state to Completed.
        * @see TaskItem::MarkCompleted()
        */
        [Test]
        public void MarkCompleted_ShouldSetStateToCompleted()
        {
            _task.MarkCompleted();
            Assert.That(_task.State, Is.EqualTo(TaskState.Completed));
        }

        /**
        * @brief Test: Checks that the Start() method sets the task state to InProgress.
        * @see TaskItem::Start()
        */
        [Test]
        public void Start_ShouldSetStateToInProgress()
        {
            _task.Start();
            Assert.That(_task.State, Is.EqualTo(TaskState.InProgress));
        }

        /**
        * @brief Test: Checks that the Reset() method sets the task state to Pending from Completed.
        * @see TaskItem::Reset()
        */
        [Test]
        public void Reset_ShouldSetStateToPending_WhenTaskWasCompleted()
        {
            _task.MarkCompleted();

            _task.Reset();

            Assert.That(_task.State, Is.EqualTo(TaskState.Pending));
        }

        /**
        * @brief Test: Checks that the Reset() method sets the task state to Pending from InProgress.
        * @see TaskItem::Reset()
        */
        [Test]
        public void Reset_ShouldSetStateToPending_WhenTaskWasInProgress()
        {
            _task.Start();

            _task.Reset();

            Assert.That(_task.State, Is.EqualTo(TaskState.Pending));
        }
    }


}