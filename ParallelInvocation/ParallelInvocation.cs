using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelInvocation
{
    public static class ParallelInvocation
    {
        /// <summary>
        /// The ProcessArrayInParallelMutex
        /// </summary>
        private static readonly object ProcessArrayInParallelMutex = new object();

        /// <summary>
        /// Gets or sets the process array in parallel invocation count.
        /// </summary>
        /// <value>
        /// The process array in parallel invocation count.
        /// </value>
        private static int ProcessArrayInParallelInvocationCount {get; set;}

        /// <summary>
        /// Processes the array in parallel.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static int ProcessArrayInParallel(double[] array)
        {
            Parallel.Invoke(
                () => ProcessPartialArray(array, 0, array.Length / 2),
                () => ProcessPartialArray(array, array.Length / 2, array.Length));

            return ProcessArrayInParallelInvocationCount;
        }

        /// <summary>
        /// Processes the partial array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="begin">The begin.</param>
        /// <param name="end">The end.</param>
        private static void ProcessPartialArray(double[] array, int begin, int end)
        {
            System.Diagnostics.Debug.WriteLine(
                string.Format(
                    "Processing array from index '{0}' to '{1}' on" +
                    Environment.NewLine +
                    "Task '{2}' executed on Thread '{3}'",
                    begin,
                    end,
                    Task.CurrentId,
                    Thread.CurrentThread.ManagedThreadId));

            // CPU-intense processing of array ...
            lock (ProcessArrayInParallelMutex)
            {
                ++ProcessArrayInParallelInvocationCount;
            }
        }

        /// <summary>
        /// Does the action20 times.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void DoAction20Times(Action action)
        {
            var actions = Enumerable.Repeat(action, 20).ToArray();
            Parallel.Invoke(actions);
        }

        /// <summary>
        /// Does the action20 times with cancellation.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="token">The token.</param>
        public static void DoAction20TimesWithCancellation(Action action, CancellationToken token)
        {
            var actions = Enumerable.Repeat(action, 20).ToArray();
            Parallel.Invoke(new ParallelOptions {CancellationToken = token}, actions);
        }
    }
}
