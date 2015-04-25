using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDelayAndRetry.Tests
{
    /// <summary>
    /// Tests for class <see cref="TaskDelayAndRetry"/>
    /// </summary>
    [TestFixture]
    class TaskDelayAndRetryTests
    {
        /// <summary>
        /// Tests that a delayed result is yielded back asynchronously.
        /// </summary>
        [Test]
        public async void DelayResult_TimeSpanSet_ReturnsDelayedResult()
        {
            // ARRANGE
            TimeSpan timespan = TimeSpan.FromSeconds(2);
            const int ExpectedResult = 100;

            // ACT
            var result = await TaskDelayAndRetry.DelayResult(result: ExpectedResult, delay: timespan);
            
            // ASSERT
            Assert.AreEqual(ExpectedResult, result);
        }

        /// <summary>
        /// Tests that the html uri string gets asynchronously returned by the method under
        /// test.
        /// 
        /// NOTE: To test the timeout you have to fake the httpclient with MSFakes to 
        /// establish the same result if fakes supports await-able methods.
        /// </summary>
        [Test]
        public async void DownloadStringWithTimeout_UriSetNoTimeOut_ReturnsFetchedUriString()
        {
            // ARRANGE
            // - nothing to arrange

            // ACT
            var result = await TaskDelayAndRetry.DownloadStringWithTimeout(uri: "http://stephencleary.com/");
            
            // ASSERT
            Assert.NotNull(result, "result is null.");
            
            var index = result.IndexOf(
                "http://stephencleary.com", 
                0, 
                result.Length, 
                StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(-1, index, "index has unexpected value.");
        }

        /// <summary>
        /// Downloads the string with retries.
        /// 
        /// Note: To test the timeout simply disconnect from your network and debug the test.
        /// You can fake the httpclient to with MSFakes to establish the same result if fakes
        /// supports await-able methods.
        /// 
        /// In the Output Window you should see something like this:
        /// 
        /// Exception occured at Retry Fetch Attempt '1'.
        /// Message: An error occurred while sending the request.
        /// A first chance exception of type 'System.Net.Http.HttpRequestException' occurred in mscorlib.dll
        /// Exception occured at Retry Fetch Attempt '2'.
        /// Message: An error occurred while sending the request.
        /// </summary>
        [Test]
        public async void DownloadStringWithRetries_UriSet_ReturnsFetchedUriString()
        {
            // ARRANGE
            // - nothing to arrange

            // ACT
            var result = await TaskDelayAndRetry.DownloadStringWithRetries(uri: "http://stephencleary.com/");

            // ASSERT
            Assert.NotNull(result, "result is null.");

            var index = result.IndexOf(
                "http://stephencleary.com",
                0,
                result.Length,
                StringComparison.OrdinalIgnoreCase);

            Assert.AreNotEqual(-1, index, "index has unexpected value.");
        }
        
    }
}
