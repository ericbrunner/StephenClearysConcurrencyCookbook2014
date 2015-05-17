using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ParallelInvocation.Tests
{
    /// <summary>
    /// Tests class <see cref="ParallelInvocation"/>.
    /// </summary>
    [TestFixture]
    public class ParallelInvocationTests
    {
        /// <summary>
        /// Tests that the array is processed in parallel.
        /// </summary>
        [Test]
        public void ProcessArrayInParallel_ProvidedValues_ExecutesPartialProcessingInParallel()
        {
            // ARRANGE
            int ExpectedResult = 2;
            var array = new double[] {4.45, 2.11, 5.34, 6.56};

            // ACT
            var result = ParallelInvocation.ProcessArrayInParallel(array);

            // ASSERT
            Assert.AreEqual(ExpectedResult, result, "result has unexpected value.");
        }

        /// <summary>
        /// Tests that the provided action is 20 times executed in parallel.
        /// </summary>
        [Test]
        public void DoAction20Times_ProvidedAction_Executed20TimesInParallel()
        {
            // ARRANGE
            int ExpectedResult = 20;
            int invocationCount = 0;
            var mutex = new object();

            Action action = () =>
            {
                System.Diagnostics.Debug.WriteLine(
                string.Format(
                    "Executes action in Task '{0}' executed on Thread '{1}'",
                    Task.CurrentId,
                    Thread.CurrentThread.ManagedThreadId));

                lock (mutex)
                {
                    ++invocationCount;
                }
            };

            // ACT
            ParallelInvocation.DoAction20Times(action);

            // ASSERT
            Assert.AreEqual(ExpectedResult, invocationCount, "result has unexpected value.");
        }

        /// <summary>
        /// Tests that the method under test thrown an OperationCanceledException.
        /// </summary>
        [Test]
        public void DoAction20TimesWithCancellation_Cancelled_ThrowsOperationCanceledException()
        {
            // ARRANGE
            Exception exception = null;
            CancellationTokenSource cts = new CancellationTokenSource();

            Action action = () =>
            {
                System.Diagnostics.Debug.WriteLine(
                string.Format(
                    "Executes action in Task '{0}' executed on Thread '{1}'",
                    Task.CurrentId,
                    Thread.CurrentThread.ManagedThreadId));

                Thread.Sleep(500);
            };

            Task.Run(() =>
            {
                Thread.Sleep(600);
                cts.Cancel();
            });

            
            // ACT
            try
            {
                ParallelInvocation.DoAction20TimesWithCancellation(action, cts.Token);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            // ASSERT
            Assert.NotNull(exception, "exception is null.");
            Assert.IsInstanceOf<OperationCanceledException>(exception, "exception has unexpected type.");
        }
    }
}
