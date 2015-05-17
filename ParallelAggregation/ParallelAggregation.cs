using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelAggregation
{
    public static class ParallelAggregation
    {

        /// <summary>
        /// Note: this is not the most efficient implementation. This isjust an example of using
        /// a lock to protect shared state.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static int ParallelSum(IEnumerable<int> values)
        {
            object mutext = new object();
            int result = 0;

            Parallel.ForEach(
                source: values,
                localInit: () => 0,
                body: (item, state, localValue) => localValue + item,
                localFinally: (localValue) =>
                {
                    // localFinally is invoke for each working task.
                    lock (mutext)
                    {
                        result += localValue;
                    }
                });

            return result;
        }

        /// <summary>
        /// Aggregates the provided values using Prallel LINQ.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static int ParallelSumWithPLINQ(IEnumerable<int> values)
        {
            return values.AsParallel().Sum();
        }

        /// <summary>
        /// Aggregates the provided values using Prallel LINQ.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static int ParallelSumWithPLINQUsingGenerics(IEnumerable<int> values)
        {
            return values.AsParallel().Aggregate(seed: 0, func: (sum, item) => sum + item);
        }
    }
}
