using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ParallelProcessingOfData.Interfaces;

namespace ParallelProcessingOfData
{
    public class ParallelProcessingOfData
    {
        /// <summary>
        /// Rotates the matrices.
        /// </summary>
        /// <param name="matrices">The matrices.</param>
        /// <param name="degree">The degree.</param>
        /// <returns></returns>
        public static ParallelLoopResult RotateMatrices(IEnumerable<IMatrix> matrices, float degree)
        {
            ParallelLoopResult parallelLoopResult = 
                Parallel.ForEach(
                    matrices,
                    (matrix, state, index) =>
                    {
                        // simulate a bit of CPU-Bound processing time
                        matrix.Rotate(degree);

                        System.Diagnostics.Debug.WriteLine(
                            string.Format(
                            "Parallel Iteration: '{0}' is running on Thread: '{1}' " + 
                            Environment.NewLine + 
                            "ParallelLoopState '{2}'", Thread.CurrentThread.ManagedThreadId, index, state));
                    });

            return parallelLoopResult;
        }

        /// <summary>
        /// Inverts the matrices.
        /// </summary>
        /// <param name="matrices">The matrices.</param>
        /// <returns></returns>
        public static ParallelLoopResult InvertMatrices(IEnumerable<IMatrix> matrices)
        {
            ParallelLoopResult parallelLoopResult =
                Parallel.ForEach(
                matrices,
                    (matrix, state, index) =>
                    {
                        // simulate a bit of CPU-Bound processing time

                        if (!matrix.IsInvertible)
                        {
                            state.Stop();
                        }
                        else
                        {
                            matrix.Invert();
                        }

                        System.Diagnostics.Debug.WriteLine(
                            string.Format(
                            "Parallel Iteration: '{0}' is running on Thread: '{1}' " +
                            Environment.NewLine +
                            "ParallelLoopState '{2}'", Thread.CurrentThread.ManagedThreadId, index, state));
                    });

            return parallelLoopResult;
        }

        /// <summary>
        /// Rotates the matrices.
        /// </summary>
        /// <param name="matrices">The matrices.</param>
        /// <param name="degree">The degree.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static ParallelLoopResult RotateMatrices(IEnumerable<IMatrix> matrices, float degree, CancellationToken token)
        {
            ParallelLoopResult parallelLoopResult =
                Parallel.ForEach(
                    matrices,
                    new ParallelOptions { CancellationToken = token },
                    (matrix, state, index) =>
                    {
                        // simulate a bit of CPU-Bound processing time
                        matrix.Rotate(degree);

                        Thread.Sleep(500);

                        System.Diagnostics.Debug.WriteLine(
                            string.Format(
                                "Parallel Iteration: '{0}' is running on " +
                                Environment.NewLine +
                                "Task: '{1}' in Thread: '{2}'  " +
                                Environment.NewLine +
                                "ParallelLoopState '{3}'" +
                                Environment.NewLine,
                                index,
                                Task.CurrentId,
                                Thread.CurrentThread.ManagedThreadId,
                                state));
                    });

            return parallelLoopResult;
        }

        /// <summary>
        /// Inverts the state of the matrices shared.
        /// </summary>
        /// <param name="matrices">The matrices.</param>
        /// <returns></returns>
        public static int InvertMatricesSharedState(IEnumerable<IMatrix> matrices)
        {
            object mutex = new object();
            int nonInvertableCount = 0;

            ParallelLoopResult parallelLoopResult =
                Parallel.ForEach(
                    matrices,
                    (matrix, state, index) =>
                    {

                        if (matrix.IsInvertible)
                        {
                            matrix.Invert();
                        }
                        else
                        {
                            lock (mutex)
                            {
                                ++nonInvertableCount;
                            }
                        }
                    });

            System.Diagnostics.Debug.WriteLine(
                string.Format(
                    "Parallel.ForEach Result:" +
                    Environment.NewLine +
                    "IsCompleted: '{0}'" +
                    Environment.NewLine +
                    "LowestBreakIteration: '{1}'",
                    parallelLoopResult.IsCompleted,
                    parallelLoopResult.LowestBreakIteration));

            return nonInvertableCount;
        }
    }
}
