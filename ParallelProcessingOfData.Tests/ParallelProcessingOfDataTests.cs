using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParallelProcessingOfData.Interfaces;

namespace ParallelProcessingOfData.Tests
{
    [TestClass]
    public class ParallelProcessingOfDataTests
    {
        [TestMethod]
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
            Assert.IsTrue(result.IsCompleted, "result.IsCompleted has unexpected value.");
        }
    }
}
