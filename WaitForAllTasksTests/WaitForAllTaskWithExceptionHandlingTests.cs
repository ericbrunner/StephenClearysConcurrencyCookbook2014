using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace WaitForAllTasks.Tests
{
    /// <summary>
    /// Tests class <see cref="WaitForAllTaskWithExceptionHandling"/>.
    /// </summary>
    [TestFixture]
    public class WaitForAllTaskWithExceptionHandlingTests
    {
        /// <summary>
        /// Tests that the async method under test throws an NotImplementedException.
        /// </summary>
        [Test]
        public async void ThrowNotImplementedExceptionAsync_NoCondition_ThrowsException()
        {
            // ARRANGE
            Exception exception = null;

            // ACT
            try
            {
                await WaitForAllTaskWithExceptionHandling.ThrowNotImplementedExceptionAsync();
            }
            catch (Exception e)
            {
                exception = e;
            }

            // ASSERT
            Assert.NotNull(exception, "exception is null.");
            Assert.IsInstanceOf<NotImplementedException>(exception, "exception has unexpected type.");
        }

        /// <summary>
        /// Tests that the async method under test throws an InvalidOperationException.
        /// </summary>
        [Test]
        public async void ThrowInvalidOperationExceptionAsync_NoCondition_ThrowsException()
        {
            // ARRANGE
            Exception exception = null;

            // ACT
            try
            {
                await WaitForAllTaskWithExceptionHandling.ThrowInvalidOperationExceptionAsync();
            }
            catch (Exception e)
            {
                exception = e;
            }

            // ASSERT
            Assert.NotNull(exception, "exception is null.");
            Assert.IsInstanceOf<InvalidOperationException>(exception, "exception has unexpected type.");
        }

        /// <summary>
        /// Tests that an exception of type 'NotImplementedException' or 
        /// 'InvalidOperationException' is thrown.
        /// </summary>
        [Test]
        public async void ObserveOneExceptionAsync_NoCondition_ThrowsException()
        {
            // ARRANGE
            Exception exception = null;

            // ACT
            Task resultTask = WaitForAllTaskWithExceptionHandling.ObserveOneExceptionAsync();
            try
            {
                await resultTask;
            }
            catch (Exception e)
            {
                exception = e;

                System.Diagnostics.Debug.WriteLine(
                    string.Format("Task completed in Status: '{0}'",
                                    resultTask.Status));
            }

            // ASSERT
            Assert.NotNull(exception, "exception is null.");

            var IsValidExceptionType = (exception is NotImplementedException) ||
                                       (exception is InvalidOperationException);

            Assert.True(IsValidExceptionType, "IsValidExceptionType has unexpected value.");
        }

        /// <summary>
        /// Tests that an exception of type 'AggregateException' is thrown.
        /// </summary>
        [Test]
        public async void ObserveAllExceptionAsync_NoCondition_ThrowsAggregateException()
        {
            // ARRANGE
            Exception exception = null;

            // ACT
            Task resultTask = WaitForAllTaskWithExceptionHandling.ObserveAllExceptionAsync();
            try
            {
                await resultTask;
            }
            catch (Exception e)
            {
                exception = e;
                System.Diagnostics.Debug.WriteLine(
                    string.Format("Task completed in Status: '{0}'",
                                    resultTask.Status));
            }

            // ASSERT
            Assert.NotNull(exception, "exception is null.");
            Assert.IsInstanceOf<AggregateException>(exception, "exception has unexpected type.");

            var aggregateException = (exception as AggregateException);
      
            Assert.AreEqual(2, aggregateException.InnerExceptions.Count,
                "aggregateException.InnerExceptions.Count has unexpected value.");

            var notImplementedException = 
                aggregateException.InnerExceptions
                .FirstOrDefault(innerException => innerException.GetType() == typeof(NotImplementedException));
            
            Assert.IsInstanceOf<NotImplementedException>(notImplementedException,
                "notImplementedException has unexpected type.");

            var invalidOperationException =
                aggregateException.InnerExceptions
                .FirstOrDefault(innerException => innerException.GetType() == typeof(InvalidOperationException));

            Assert.IsInstanceOf<InvalidOperationException>(invalidOperationException,
                "notImplementedException has unexpected type.");

        }

    }
}
