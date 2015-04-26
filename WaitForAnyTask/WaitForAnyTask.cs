using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WaitForAnyTask
{
    public static class WaitForAnyTask
    {

        /// <summary>
        /// Returns the data length of the first completed task.
        /// </summary>
        /// <param name="urlA">The URL a.</param>
        /// <param name="urlB">The URL b.</param>
        /// <returns></returns>
        public static async Task<int> FirstRespondingUrlAsync(string urlA, string urlB)
        {
            var httpClient = new HttpClient();

            // Start both downloads concurrently
            Task<byte[]> downloadTaskA = httpClient.GetByteArrayAsync(urlA);
            Task<byte[]> downloadTaskB = httpClient.GetByteArrayAsync(urlB);

            // Wait for either of the tasks to complete.
            Task<byte[]> completedTask = await Task.WhenAny(downloadTaskA, downloadTaskB);

            System.Diagnostics.Debug.WriteLine(
                string.Format("Completed Task is: '{0}'",
                    (completedTask == downloadTaskA) ? "downloadTaskA" : "downloadTaskB"));

            // Await the returned Task
            byte[] data = await completedTask;

            // Return the data length
            return data.Length;
        }
    }
}
