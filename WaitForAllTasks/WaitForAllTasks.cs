using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WaitForAllTasks
{
    /// <summary>
    /// Implements some methods to demonstrate the usage of Task.WhenAll.
    /// </summary>
    public static class WaitForAllTasks
    {
        /// <summary>
        /// This method waits for all 3 delay task to complete.
        /// Task2 takes longer as Task1 and Task3. But that doesn't
        /// matter. Task.Whenall completed only if 'ALL' tasks have
        /// completed.
        /// </summary>
        /// <returns></returns>
        public static async Task WaitForSomeDelayTasksAsync()
        {
            var task1 = Task.Delay(TimeSpan.FromSeconds(1));
            var task2 = Task.Delay(TimeSpan.FromSeconds(2));
            var task3 = Task.Delay(TimeSpan.FromSeconds(1));

            await Task.WhenAll(task1, task2, task3);
        }

        /// <summary>
        /// Waits for all integer returning tasks to complete.
        /// </summary>
        /// <returns></returns>
        public static async Task<int[]> WaitForIntegerResultsAsync()
        {
            var task1 = Task.FromResult(3);
            var task2 = Task.FromResult(5);
            var task3 = Task.FromResult(7);

            var task4 = GetResultAsync(17, TimeSpan.FromSeconds(2));

            return await Task.WhenAll(task1, task2, task3, task4);
        }

        /// <summary>
        /// Gets the result asynchronous.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="delay">The delay.</param>
        /// <returns></returns>
        public static async Task<int> GetResultAsync(int value, TimeSpan delay)
        {
            await Task.Delay(delay);
            return value;
        }

        public static async Task<string> DownloadAllAsync(IEnumerable<string> urls)
        {
            var httpClient = new HttpClient();

            // Define what we are going to do here for each url.
            var downloads = urls.Select(url => httpClient.GetStringAsync(url));

            /*
             * NOTE: At that point no tasks have actually started yet
             * because the LINQ sequence is not executed yet. That happens
             * in the next statement.
             * 
             */

            // Start all URLs downloading simulanously because the above
            // LINQ query gets executed ith ToArray (get materialized) here. 
            Task<string>[] downloadTasks = downloads.ToArray();

            /*
             * NOTE: Now the tasks have all started 
             * 
             */

            // Asynchronously wait for all download tasks to complete.
            string[] htmlPages = await Task.WhenAll(downloadTasks);

            return string.Concat(htmlPages);
        }
    }
}
