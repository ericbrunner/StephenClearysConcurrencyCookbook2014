using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTasksAsTheyComplete
{
    public class ProcessingTasksAsTheyComplete
    {
        /// <summary>
        /// Delays the and return asynchronous.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        private static async Task<int> DelayAndReturnAsync(int val)
        {
            await Task.Delay(TimeSpan.FromSeconds(val));
            return val;
        }


        /// <summary>
        /// Currently this method prints "2", "3", and "1".
        /// </summary>
        /// <returns>a list of unordered results</returns>
        public static async Task<List<int>> ProcessTasksAsync()
        {
            var results = new List<int>();
            
            // Create a sequence of tasks.
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);
            Task<int> taskC = DelayAndReturnAsync(1);

            var tasks = new[] {taskA, taskB, taskC};

            foreach (var task in tasks)
            {
                var result = await task;
                results.Add(result);
                System.Diagnostics.Debug.WriteLine(result);
            }

            return results;
        }

        private static async Task<int> AwaitAndProcessAsync(Task<int> task)
        {
            var result = await task;
            System.Diagnostics.Debug.WriteLine(result);
            return result;
        }

        /// <summary>
        /// This method now prints "1", "2", and "3".
        /// </summary>
        /// <returns></returns>
        public static async Task<List<int>> ProcessTasksAsCompletedAsync()
        {
            List<int> results = new List<int>();

            // Create a sequence of tasks.
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);
            Task<int> taskC = DelayAndReturnAsync(1);

            var tasks = new[] { taskA, taskB, taskC };

            // like written in the book (below instruction line does the same bot adds the values into a list.)
            //IEnumerable<Task<int>> processingTasks = (from t in tasks select AwaitAndProcessAsync(t)).ToArray();

            var processingTasks = tasks.Select(async (task) =>
            {
                var result = await AwaitAndProcessAsync(task);
                results.Add(result);

            }).ToArray();

            await Task.WhenAll(processingTasks);

            return results;
        }
    }
}
