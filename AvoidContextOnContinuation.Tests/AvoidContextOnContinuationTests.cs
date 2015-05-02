using System;
using NUnit.Framework;


namespace AvoidContextOnContinuation.Tests
{
    /// <summary>
    /// Tests class <see cref="AvoidContextOnContinuation"/>.
    /// </summary>
    [TestFixture]
    public class AvoidContextOnContinuationTests
    {
        /// <summary>
        /// Tests that the method under resumes its inner task continuation on 
        /// the SynchronizationContext (on the same Thread in that Context).
        /// </summary>
        [Test]
        public async void ResumeOnContextAsync()
        {
            // ARRANGE + ACT
            var continuedOnSameContext = await AvoidContextOnContinuation.ResumeOnContextAsync();

            // ASSERT
            Assert.True(continuedOnSameContext, "continuedOnSameContext has unexpected value.");
        }

        /// <summary>
        /// Tests that the method under test continues on a new ThreadPool Thread (not on the
        /// SynchronizationContext).
        /// </summary>
        [Test]
        public async void ResumeWhitoutContextAsync()
        {
            // ARRANGE + ACT
            var notContinuedOnSameContext = await AvoidContextOnContinuation.ResumeWhitoutContextAsync();

            // ASSERT
            Assert.True(notContinuedOnSameContext, "notContinuedOnSameContext has unexpected value.");
        }
    }
}
