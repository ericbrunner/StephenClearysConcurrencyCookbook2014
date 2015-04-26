using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingProgress
{
    /// <summary>
    /// Tests the IProghress<typeparamref name="T"/> behaviour of an asynchronous method.
    /// </summary>
    public class ReportingProgress
    {
        /// <summary>
        /// An await-able asynchronous method reporting progress if formal
        /// argument progress set to some instance implementing IProgress<typeparamref name="T"/>.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <returns></returns>
        public static async Task MyMethodAsync(IProgress<double> progress = null)
        {
            double percentComplete = 0;
            bool done = false;

            while (!done)
            {
                // simulate some asynchronous work (e.g. File I/O) ...
                await Task.Delay(100);

                percentComplete += 10;

                if (progress != null)
                {
                    progress.Report(percentComplete);
                }

                // exit the loop ...
                if (percentComplete == 100)
                {
                    done = true;
                }
            }
        }
    }
}
