using System;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;

namespace WaitForAllTasks.Tests
{
    /// <summary>
    /// Tests class <see cref="WaitForAllTasksTests"/>.
    /// </summary>
    [TestFixture]
    public class WaitForAllTasksTests
    {
        /// <summary>
        /// Tests that some delayed tasks are awaited without any exception.
        /// </summary>
        [Test]
        public void WaitForSomeDelayTasksAsync_NoCondition_ReturnsVoid()
        {
            // ARRANGE
            Task resultTask = null;

            // ACT + ASSERT
            Assert.DoesNotThrow(async () =>
            {
                resultTask = WaitForAllTasks.WaitForSomeDelayTasksAsync();
                await resultTask;

                System.Diagnostics.Debug.WriteLine(
                    string.Format("Task completed in Status: '{0}'",
                                    resultTask.Status));
            });

            Assert.AreEqual(TaskStatus.RanToCompletion, resultTask.Status, "resultTask.Status has unexpected value.");
        }

        /// <summary>
        /// Tests that some int32 returning tasks are awaited.
        /// </summary>
        [Test]
        public async void WaitForIntegerResultsAsync_NoCondition_ReturnsInt32Array()
        {
            // ARRANGE
            int[] expectedResult = {3, 5, 7, 17};
            int[] resultInt32Array = null;

            // ACT
            var resultTask = WaitForAllTasks.WaitForIntegerResultsAsync();
            resultInt32Array = await resultTask;

            System.Diagnostics.Debug.WriteLine(
                string.Format("Task completed in Status: '{0}'",
                                resultTask.Status));

            // ASSERT
            Assert.AreEqual(TaskStatus.RanToCompletion, resultTask.Status, "resultTask.Status has unexpected value.");
            Assert.NotNull(resultInt32Array, "resultInt32Array is null.");
            Assert.True(expectedResult.SequenceEqual(resultInt32Array), "resultInt32Array has unexpected values.");
        }

        /// <summary>
        /// Tests that the html content of 3 different internet sites are downloaded
        /// asynchronously and the resulted Task of Task.WhenAll completes in State
        /// 'RanToCompletion'.
        /// </summary>
        [Test]
        public async void DownloadAllAsync_Set3Urls_ReturnsHtmlStringOf3DownloadedUrls()
        {
            // ARRANGE
            string[] urls = { "http://www.google.com", "http://www.youtube.com", "http://blog.stephencleary.com/" };

            // ACT
            var downloadAllAsyncTask = WaitForAllTasks.DownloadAllAsync(urls);

            var downloadedHtmlContent = await downloadAllAsyncTask;

            System.Diagnostics.Debug.WriteLine(
                string.Format("Task completed in Status: '{0}'",
                                downloadAllAsyncTask.Status));
            
            // ASSERT
            Assert.AreEqual(TaskStatus.RanToCompletion, downloadAllAsyncTask.Status, 
                "downloadAllAsyncTask.Status has unexpected value.");
            Assert.NotNull(downloadedHtmlContent, "downloadedHtmlContent is null.");
            
            System.Diagnostics.Debug.WriteLine(
                string.Format("Downloaded HTML: " + 
                              Environment.NewLine  + 
                              "{0}",
                              downloadedHtmlContent));
        }
    }
}
