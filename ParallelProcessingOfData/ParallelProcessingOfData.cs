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
        public static ParallelLoopResult RotateMatrices(IEnumerable<IMatrix> matrices, float degree)
        {
            ParallelLoopResult parallelLoopResult = 
                Parallel.ForEach(matrices,
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
    }
}
