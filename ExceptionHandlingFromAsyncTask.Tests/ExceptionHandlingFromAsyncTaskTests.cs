using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ExceptionHandlingFromAsyncTask.Tests
{
    /// <summary>
    /// Tests class <see cref="ExceptionHandlingFromAsyncTask"/>.
    /// </summary>
    [TestFixture]
    public class ExceptionHandlingFromAsyncTaskTests
    {
        /// <summary>
        /// Tests that the exception is thrown on the awaited task returned by the 
        /// method under test, an propageted to the test method.
        /// </summary>
        [Test]
        public async void ThrowExceptionAsync_NoCondition_ThrowsExceptionOnAwaitedTask()
        {
            // ARRANGE
            Exception exception = null;

            //ACT
            try
            {
                await ExceptionHandlingFromAsyncTask.ThrowExceptionAsync();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // ASSERT
            Assert.NotNull(exception, "exception is null.");
            Assert.IsInstanceOf<InvalidOperationException>(exception, "exception has unexpected type.");
        }

        /// <summary>
        /// Tests that the exception is re-thrown on the awaited task an propageted to
        /// the test method.
        /// </summary>
        [Test]
        public async void ThrowExceptionAsync_ReturnedTaskAwaited_ThrowsExceptionOnAwaitedTask()
        {
            // ARRANGE
            Exception exception = null;

            //ACT

            // The exception is thrown by the method and placed on the task.
            Task task = ExceptionHandlingFromAsyncTask.ThrowExceptionAsync();

            try
            {
                // The exception is reraised here, where the task is awaited.
                await task;
            }
            catch (Exception ex)
            {
                // The exception is correctly caught here.
                exception = ex;
            }

            // ASSERT
            Assert.NotNull(exception, "exception is null.");
            Assert.IsInstanceOf<InvalidOperationException>(exception, "exception has unexpected type.");
        }
    }
}
