using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ParallelProcessingOfData.Interfaces;

namespace ParallelProcessingOfData.Tests
{
    [TestFixture]
    public class ParallelProcessingOfDataTests
    {
        /// <summary>
        /// Tests that the the Parallel.ForEach loop(s) is completed.
        /// </summary>
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

        /// <summary>
        /// Tests that the the Parallel.ForEach loop(s) is not completed.
        /// </summary>
        [Test]
        public void InvertMatrices_AddedSomeMatrices_ReturnsLoopResultNotCompleted()
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

        /// <summary>
        /// Tests that the the Parallel.ForEach loop(s) is completed.
        /// </summary>
        [Test]
        public void InvertMatrices_AddedSomeMatrices_ReturnsLoopResultCompleted()
        {
            // ARRANGE
            var mockMatrix = new Mock<IMatrix>();

            mockMatrix.Setup(context => context.IsInvertible).Returns(true);

            List<IMatrix> matrices = new List<IMatrix>();

            for (int i = 0; i < 100; i++)
            {
                matrices.Add(mockMatrix.Object);
            }

            // ACT
            ParallelLoopResult result = ParallelProcessingOfData.InvertMatrices(matrices);

            // ASSERT
            Assert.True(result.IsCompleted, "result.IsCompleted has unexpected value.");
        }

        [Test]
        public void RotateMatrices_AddedCancellationToken_ThrowsOperationCancelledException()
        {
            // ARRANGE
            var mockMatrix = new Mock<IMatrix>();

            mockMatrix.Setup(context => context.IsInvertible).Returns(true);

            List<IMatrix> matrices = new List<IMatrix>();

            for (int i = 0; i < 100; i++)
            {
                matrices.Add(mockMatrix.Object);
            }

            var cts = new CancellationTokenSource();

            // ACT

            Task.Factory.StartNew(
                () =>
                {
                    Thread.Sleep(3000);
                    cts.Cancel();
                });

            Exception exception = null;
            ParallelLoopResult result;
            try
            {
                result = ParallelProcessingOfData.RotateMatrices(matrices, 45f, cts.Token);
            }
            catch (Exception ex)
            {
                exception = ex;
                System.Diagnostics.Debug.WriteLine(
                    string.Format("Parallel loop was cancelled an raised '{0}'",ex.GetType().Name));
            }

            // ASSERT
            Assert.NotNull(exception, "exception is null.");
            Assert.IsInstanceOf<OperationCanceledException>(exception, "exception has unexpected type.");
        }
    }
}
