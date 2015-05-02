using System;
using NUnit.Framework;
using System.Windows.Input;
using Nito.AsyncEx;

namespace ExceptionHandlingOnAsyncVoid.Tests
{
    /// <summary>
    /// Tests class <see cref="ExceptionHandlingOnAsyncVoid"/>.
    /// </summary>
    [TestFixture]
    public class ExceptionHandlingOnAsyncVoidTests
    {
        /// <summary>
        /// Tests that the method under test gets awaited and throw an
        /// InvalidOperationException.
        /// </summary>
        [Test]
        public async void InnerExecute_NoCondition_ThrowsException()
        {
            // ARRANGE
            Exception exception = null;
            var objectUnderTest = new ExceptionHandlingOnAsyncVoid.MyAsyncCommand();

            // ACT
            try
            {
                await objectUnderTest.InnerExecute("some argument");
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
        /// Tests that the exception thrown in the continuation part of the awaited inner task
        /// is not caught, because an async void method can't be awaited. Next test method
        /// uses the Nito.AsynEx nuget package to catch the exception.
        /// </summary>
        [Test]
        public void ExecuteWithoutTask_NoCondition_NoExceptionCaught()
        {
            // ARRANGE
            Exception exception = null;
            var objectUnderTest = new ExceptionHandlingOnAsyncVoid.MyAsyncCommand();

            // ACT
            try
            {
                objectUnderTest.ExecuteWithoutTask("some argument");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // ASSERT
            Assert.IsNull(exception, "exception is not null.");
        }

        /// <summary>
        /// Tests that the exception thrown in the continuation part of the awaited inner task
        /// is not caught, because an async void method can't be awaited.
        /// </summary>
        [Test]
        public void ExecuteWithoutTask_UseNitoAsyncContext_ExceptionCaught()
        {
            // ARRANGE
            Exception exception = null;
            var objectUnderTest = new ExceptionHandlingOnAsyncVoid.MyAsyncCommand();

            // ACT
            try
            {
                AsyncContext.Run(() => objectUnderTest.ExecuteWithoutTask("some argument"));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // ASSERT
            Assert.NotNull(exception, "exception is null.");
            Assert.IsInstanceOf<InvalidOperationException>(exception, "exception has unexpected type.");
        }
        
    }
}
