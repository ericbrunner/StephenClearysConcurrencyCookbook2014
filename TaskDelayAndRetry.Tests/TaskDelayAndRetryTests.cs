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
        
    }
}
