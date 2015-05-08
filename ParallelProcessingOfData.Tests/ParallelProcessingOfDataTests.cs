using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ParallelProcessingOfData.Interfaces;

namespace ParallelProcessingOfData.Tests
{
    /// <summary>
    /// Tests class <see cref="ParallelProcessingOfData"/>.
    /// </summary>
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

        /// <summary>
        /// Test that an OperationCanceledException is thrown if the Parallel.ForEach loop
        /// is cancelled from outside the loop with the provided CancellationToken.
        /// </summary>
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
            try
            {
                ParallelProcessingOfData.RotateMatrices(matrices, 45f, cts.Token);
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

        /// <summary>
        /// Inverts the matrices shared state_ added4 non invertible matrices_ returned conut of non invertable matrices.
        /// </summary>
        [Test]
        public void InvertMatricesSharedState_Added4NonInvertibleMatrices_ReturnedConutOfNonInvertableMatrices()
        {
            // ARRANGE
            Mock<IMatrix> matrix;
            var matrices = new List<IMatrix>();

            for (int i = 0; i < 10000; i++)
            {
                matrix = new Mock<IMatrix>();

                if (i % 2 == 0)
                {
                    matrix.Setup(context => context.IsInvertible).Returns(true);
                }
                else
                {
                    matrix.Setup(context => context.IsInvertible).Returns(false);
                }

                matrices.Add(matrix.Object);
            }

            // ACT
            int nonInvertableMatrices = ParallelProcessingOfData.InvertMatricesSharedState(matrices);

            // ASSERT
            Assert.AreEqual(5000, nonInvertableMatrices, "nonInvertableMatrices has an unexpected value.");
        }
    }
}
