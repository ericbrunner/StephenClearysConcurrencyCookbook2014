using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace CompletedTasks.Tests
{
    /// <summary>
    /// Tests class <see cref="MySynchronousImplementation"/>
    /// </summary>
    [TestFixture]
    public class MySynchronousImplementationTests
    {
        /// <summary>
        /// Tests that an already completed Task is awaited and yields the int32 value '0'.
        /// </summary>
        [Test]
        public async void GetZeroValueAsync_NoCondition_ReturnsCompletedTask0()
        {
            // ARRANGE
            const int ExpectedResult = 0;

            // ACT
            var result = await MySynchronousImplementation.GetZeroValueAsync();

            // ASSERT
            Assert.AreEqual(ExpectedResult, result, "result has unexpected value.");
        }

        /// <summary>
        /// Tests that an already completed Task is awaited and yields the int32 value '13'.
        /// </summary>
        [Test]
        public async void GetValueAsync_NoCondition_ReturnsCompletedTask13()
        {
            // ARRANGE
            const int ExpectedResult = 13;
            MySynchronousImplementation objectUnderTest = new MySynchronousImplementation();

            // ACT
            var result = await objectUnderTest.GetValueAsync();

            // ASSERT
            Assert.AreEqual(ExpectedResult, result, "result has unexpected value.");
        }

        /// <summary>
        /// Tests that an already completed Task is awaited and throws an NotImplementedException.
        /// </summary>
        [Test]
        public async void NotImplementatedAsync_NoCondition_ThrowsAnNotImpletementedException()
        {
            // ARRANGE
            // - nothing to arrange yet

            // ACT
            Exception exception = null;
            try
            {
                var result = await MySynchronousImplementation.NotImplementatedAsync<int>();
            }
            catch (Exception e)
            {
                exception = e;
            }

            // ASSERT
            Assert.NotNull(exception, "exception is null.");
            Assert.IsInstanceOf<NotImplementedException>(exception, "exception has unexpected type.");
        }
    }
}
