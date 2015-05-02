using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandlingFromAsyncTask
{
    /// <summary>
    /// Samples from chapter 2.8.
    /// </summary>
    public class ExceptionHandlingFromAsyncTask
    {
        /// <summary>
        /// That method returns a task and places the raised exception on that task.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Test</exception>
        public static async Task ThrowExceptionAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            // this is the continuation of above awaited delayed task.
            // that exception is placed on the task that method returns.
            throw new InvalidOperationException("Test");
        }
    }
}
