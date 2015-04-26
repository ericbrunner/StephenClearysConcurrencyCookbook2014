using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace WaitForAllTasks
{
    /// <summary>
    /// Implements some methods to demonstrate the usage of Task.WhenAll
    /// with proper exception handling.
    /// </summary>
    public static class WaitForAllTaskWithExceptionHandling
    {
        /// <summary>
        /// Throws the not implemented exception asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static async Task ThrowNotImplementedExceptionAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Throws the invalid operation exception asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static async Task ThrowInvalidOperationExceptionAsync()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Observes the exception of the first completed task.
        /// </summary>
        /// <returns></returns>
        public static async Task ObserveOneExceptionAsync()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();

            try
            {
                // Throws only one exception occured in the completed tasks.
                await Task.WhenAll(task1, task2);
            }
            catch (Exception e)
            {
                // e is either "NotImplementedException" or "InvalidOperationException"
                System.Diagnostics.Debug.Write(
                    string.Format("Exception Type: '{0}'", e.GetType().Name));

                // Propagate Exception (if required)                
                throw;
            }
        }

        /// <summary>
        /// Observes all exceptions of all completed tasks.
        /// </summary>
        /// <returns></returns>
        public static async Task ObserveAllExceptionAsync()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();

            var allTasks = Task.WhenAll(task1, task2);

            try
            {
                // Exceptions of all tasks are 'aggregated' on the returned task from
                // Task.Whenall and set on the property 'Exception'.
                await allTasks;
            }
            catch 
            {
                AggregateException allExceptions = allTasks.Exception;

                foreach (var exception in allExceptions.InnerExceptions)
	            {
                    System.Diagnostics.Debug.WriteLine(
                        string.Format("Exception Type: '{0}'", 
                        exception.GetType().Name));
                };
                
                // Propagate Exception (if required)                
                ExceptionDispatchInfo.Capture(allExceptions).Throw();
            }
        }

    }
}
