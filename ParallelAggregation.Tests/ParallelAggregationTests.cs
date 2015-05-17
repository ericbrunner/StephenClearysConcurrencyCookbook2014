using System;
using NUnit.Framework;

namespace ParallelAggregation.Tests
{
    /// <summary>
    /// Tests class <see cref="ParallelAggregation"/>.
    /// </summary>
    [TestFixture]
    public class ParallelAggregationTests
    {
        /// <summary>
        /// Tests that the method under test aggregates all values in a
        /// parallel processing way and returns the result.
        /// </summary>
        [Test]
        public void ParallelSum_ProvidedValues_ReturnsSumOfValues()
        {
            // ARRANGE
            var values = new int[] {10, 20, 30, 40, 50, 60};
            int ExpectedSum = 210;
            // ACT
            var result = ParallelAggregation.ParallelSum(values);

            // ASSERT
            Assert.AreEqual(ExpectedSum, result, "result has unexpected value.");
        }

        /// <summary>
        /// Tests that the method under test aggregates all values in a
        /// parallel processing way using PLINQ and returns the result.
        /// </summary>
        [Test]
        public void ParallelSumWithPLINQ_ProvidedValues_ReturnsSumOFValues()
        {
            // ARRANGE
            var values = new int[] { 10, 20, 30, 40, 50, 60 };
            int ExpectedSum = 210;
            // ACT
            var result = ParallelAggregation.ParallelSumWithPLINQ(values);

            // ASSERT
            Assert.AreEqual(ExpectedSum, result, "result has unexpected value.");
        }

        /// <summary>
        /// Tests that the method under test aggregates all values in a
        /// parallel processing way using PLINQ and returns the result.
        /// </summary>
        [Test]
        public void ParallelSumWithPLINQUsingGenerics_ProvidedValues_ReturnsSumOFValues()
        {
            // ARRANGE
            var values = new int[] { 10, 20, 30, 40, 50, 60 };
            int ExpectedSum = 210;
            // ACT
            var result = ParallelAggregation.ParallelSumWithPLINQUsingGenerics(values);

            // ASSERT
            Assert.AreEqual(ExpectedSum, result, "result has unexpected value.");
        }
    }
}
