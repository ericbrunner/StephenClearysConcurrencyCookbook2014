using NUnit.Framework;
using System;
using System.Threading.Tasks;


namespace ReportingProgress.Tests
{
    /// <summary>
    /// Tests class <see cref="ReportingProgress"/>
    /// </summary>
    [TestFixture]
    public class ReportingProgressTests
    {
        /// <summary>
        /// Tests that the method under test fired the progress event 10 times.
        /// </summary>
        [Test]
        public async void MyMethodAsync_ProgressHandlerSet_ProgressEventFired()
        {
            // ARRANGE
            const double ExpectedPercentage = 100;
            double percentageComplete = 0;
            var progress = new Progress<double>();
            progress.ProgressChanged += (sender, percentage) => 
            {
                percentageComplete = percentage;
                System.Diagnostics.Debug.WriteLine("Complete Percentage '{0}%'", percentageComplete);
            };

            // ACT
            await ReportingProgress.MyMethodAsync(progress);

            /*
             * If you run the test and look in the Output window 
             * the Percentage is 100% but the test fails.
             * 
             * Stephen Cleary mentioned in the discussion section the asynchronously
             * reporting behaviour. 
             * 
             * If the Task returned by MyMethodAsync has completed, the unit test
             * continous with the next operation which is the Assert.AreEquals(...)
             * below in the calling code of the unit test.
             * 
             * At that time the 'percentageComplete' variable may have not been
             * updated from the asynchrously invoked progress eventhandler.
             * 
             * If you uncomment the Task.Delay below it should be enought time to get 
             * the 'percentageComplete' to be updated in the eventhandler and the
             * Assert should pass.
             *  
             * */
            
            await Task.Delay(100);

            // ASSERT
            Assert.AreEqual(ExpectedPercentage, percentageComplete, "percentageComplete has unexpected value.");
        }
    }
}
