using System;
using NUnit.Framework;


namespace AvoidContextOnContinuation.Tests
{
    [TestFixture]
    public class AvoidContextOnContinuationTests
    {
        [Test]
        public async void ResumeOnContextAsync()
        {
            // ARRANGE + ACT
            var continuedOnSameContext = await AvoidContextOnContinuation.ResumeOnContextAsync();

            // ASSERT
            Assert.True(continuedOnSameContext, "continuedOnSameContext has unexpected value.");
        }

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
