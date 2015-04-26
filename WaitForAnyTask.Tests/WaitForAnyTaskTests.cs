using System;
using NUnit.Framework;

namespace WaitForAnyTask.Tests
{
    /// <summary>
    /// Tests class <see cref="WaitForAnyTask"/>.
    /// </summary>
    [TestFixture]
    public class WaitForAnyTaskTests
    {
        /// <summary>
        /// Tests that the downloaded html data length from the first completed task is retured.
        /// NOTE: Run test several times to see in the Output Window something like that:
        /// 
        /// Completed Task is: 'downloadTaskA'
        /// 
        /// next test run ...
        /// Completed Task is: 'downloadTaskB'
        /// 
        /// </summary>
        [Test]
        public async void FirstRespondingUrlAsync_TwoEqualURLs_ReturnsDataLengthOfFirstCompletedTask()
        {
            // ARRANGE
            const string urlA = "https://msdn.microsoft.com", urlB = "https://msdn.microsoft.com";

            // ACT
            var resultTask = WaitForAnyTask.FirstRespondingUrlAsync(urlA, urlB);

            var result = await resultTask;

            System.Diagnostics.Debug.WriteLine(
                string.Format("Task completed in Status: '{0}'", resultTask.Status));

            // ASSERT
            Assert.AreNotEqual(default(int), result, "result has unexpected value.");
        }
    }
}
