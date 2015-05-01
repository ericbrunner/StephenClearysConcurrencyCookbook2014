using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ProcessingTasksAsTheyComplete.Tests
{
    /// <summary>
    /// Tests for class <see cref="ProcessingTasksAsTheyComplete"/>.
    /// </summary>
    [TestFixture]
    public class ProcessingTasksAsTheyCompleteTests
    {
        /// <summary>
        /// Processes the tasks async_ no condition_ returns un ordered ints.
        /// </summary>
        [Test]
        public async void ProcessTasksAsync_NoCondition_ReturnsUnOrderedInts()
        {
            // ARRANGE
            List<int> expectedResult = new List<int> { 2, 3, 1};

            // ACT
            var result = await ProcessingTasksAsTheyComplete.ProcessTasksAsync();

            // ASSERT
            Assert.True(expectedResult.SequenceEqual(result), "result has unexpected values.");
        }

        /// <summary>
        /// Awaits the and process async_ no condition_ returns un ordered ints.
        /// </summary>
        [Test]
        public async void ProcessTasksAsCompletedAsync_NoCondition_ReturnsUnOrderedInts()
        {
            // ARRANGE
            List<int> expectedResult = new List<int> { 1, 2, 3 };

            // ACT
            var result = await ProcessingTasksAsTheyComplete.ProcessTasksAsCompletedAsync();

            // ASSERT
            Assert.True(expectedResult.SequenceEqual(result), "result has unexpected values.");
        }
    }
}
