using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace TaskDelayAndRetry
{
    /// <summary>
    /// That class covers chapter 2.1 "Pausing for a Period of Time.
    /// </summary>
    public static class TaskDelayAndRetry
    {
        /// <summary>
        /// Delays the result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The result.</param>
        /// <param name="delay">The delay.</param>
        /// <returns></returns>
        public static async Task<T> DelayResult<T>(T result, TimeSpan delay)
        {
            await Task.Delay(delay);
            return result;
        }

        /// <summary>
        /// Downloads the string with retries.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<string> DownloadStringWithRetries(string uri)
        {
            using (var client = new HttpClient())
            {
                // Retry after 1 second, then after 2 seconds, the 4 (math power of 2: n^2)
                var nextDelay = TimeSpan.FromSeconds(1);

                for (int i = 0; i != 3; ++i)
                {
                    try
                    {
                        return await client.GetStringAsync(uri);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            string.Format("Exception occured at Retry Fetch Attempt '{0}'." +
                            Environment.NewLine +
                            "Message: {1}",
                            i,
                            e.Message));
                        
                    }


                    await Task.Delay(nextDelay);
                    nextDelay = nextDelay + nextDelay;
                }

                // Retry one last time, allowing the error to propagate.
                return await client.GetStringAsync(uri);
            }
        }

        /// <summary>
        /// Downloads the string with timeout.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<string> DownloadStringWithTimeout(string uri)
        {
            using (var client = new HttpClient())
            {
                var downloadTask = client.GetStringAsync(uri);
                var timeoutTask = Task.Delay(3000);

                var completedTask = await Task.WhenAny(downloadTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    return null;
                }

                return await downloadTask;
            }
        }
    }
}
