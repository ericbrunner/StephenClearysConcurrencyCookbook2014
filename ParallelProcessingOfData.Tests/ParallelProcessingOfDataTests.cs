using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ParallelProcessingOfData.Interfaces;

namespace ParallelProcessingOfData.Tests
{
    [TestFixture]
    public class ParallelProcessingOfDataTests
    {
        [Test]
        public void RotateMatrices_AddedSomeMatrices_ReturnsLoopResult()
        {
            // ARRANGE
            var mockMatrix = new Mock<IMatrix>();

            List<IMatrix> matrices = new List<IMatrix>();

            for (int i = 0; i < 100; i++)
            {
                matrices.Add(mockMatrix.Object);
            }

            // ACT
            ParallelLoopResult result = ParallelProcessingOfData.RotateMatrices(matrices, 45f);

            // ASSERT
            Assert.True(result.IsCompleted, "result.IsCompleted has unexpected value.");
        }


        [Test]
        public void InvertMatrices_AddedSomeMatrices_ReturnsLoopResult()
        {
            // ARRANGE
            var mockMatrix = new Mock<IMatrix>();

            mockMatrix.Setup(context => context.IsInvertible).Returns(false);
            
            List<IMatrix> matrices = new List<IMatrix>();

            for (int i = 0; i < 100; i++)
            {
                matrices.Add(mockMatrix.Object);
            }

            // ACT
            ParallelLoopResult result = ParallelProcessingOfData.InvertMatrices(matrices);

            // ASSERT
            Assert.False(result.IsCompleted, "result.IsCompleted has unexpected value.");
        }
    }
}
