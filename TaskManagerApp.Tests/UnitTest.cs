using NUnit.Framework;
using TaskManagerApp.Models;

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

        [Test] // Метод тесту
        public void MarkCompleted_ShouldSetStateToCompleted()
        {
            _task.MarkCompleted(); 
            Assert.That(_task.State, Is.EqualTo(TaskState.Completed));
        }
    }


}