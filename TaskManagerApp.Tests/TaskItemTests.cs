using NUnit.Framework;
using TaskManagerApp.Models;
using System.Linq;

namespace TaskManagerApp.Tests
{
    [TestFixture]
    public class TaskItemTests
    {
        private TaskItem _task;

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
        public void TaskItem_ShouldNotInitializeTask_WhenTiteIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _task = new TaskItem( null, "Test Description"));
        }

        [Test]
        public void TaskItem_ShouldInitializeTaskAndSetStateToPending()
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