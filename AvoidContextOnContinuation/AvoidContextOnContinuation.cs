using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvoidContextOnContinuation
{
    /// <summary>
    /// Shows samples from Chapter 2.6.
    /// </summary>
    public class AvoidContextOnContinuation
    {
        /// <summary>
        /// This method resumes its inner task continuation on the 
        /// SynchronizationContext (on the same Thread in that Context).
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> ResumeOnContextAsync()
        {
            var synchronizationContextBefore = SynchronizationContext.Current;
            var managedThreadIdAfterBefore = Thread.CurrentThread.ManagedThreadId;

            System.Diagnostics.Debug.WriteLine(
                string.Format(
                "SynchronizationContext: {0}" +
                Environment.NewLine +
                "ManagedThreadId: {1}",
                synchronizationContextBefore,
                managedThreadIdAfterBefore));

            await Task.Delay(TimeSpan.FromSeconds(1));

            // This method resumes within the same context

            var synchronizationContextAfter = SynchronizationContext.Current;
            var managedThreadIdAfter = Thread.CurrentThread.ManagedThreadId;

            System.Diagnostics.Debug.WriteLine(
                string.Format(
                "SynchronizationContext: {0}" +
                Environment.NewLine +
                "ManagedThreadId: {1}",
                (synchronizationContextAfter != null) ?
                synchronizationContextAfter.ToString() : "is null",
                managedThreadIdAfter));

            return (synchronizationContextBefore.Equals(synchronizationContextAfter)) &&
                   (managedThreadIdAfterBefore == managedThreadIdAfter);
        }

        /// <summary>
        /// This method continues on a new ThreadPool Thread (not on the
        /// SynchronizationContext)
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> ResumeWhitoutContextAsync()
        {
            var synchronizationContextBefore = SynchronizationContext.Current;
            var managedThreadIdAfterBefore = Thread.CurrentThread.ManagedThreadId;

            System.Diagnostics.Debug.WriteLine(
                string.Format(
                "SynchronizationContext: {0}" +
                Environment.NewLine +
                "ManagedThreadId: {1}",
                synchronizationContextBefore,
                managedThreadIdAfterBefore));

            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(continueOnCapturedContext: false);

            // This method discards the context when it resumes

            var synchronizationContextAfter = SynchronizationContext.Current;
            var managedThreadIdAfter = Thread.CurrentThread.ManagedThreadId;
            
            System.Diagnostics.Debug.WriteLine(
                string.Format(
                "SynchronizationContext: {0}" +
                Environment.NewLine +
                "ManagedThreadId: {1}",
                (synchronizationContextAfter != null) ?
                synchronizationContextAfter.ToString() : "is null",
                managedThreadIdAfter));

            
            return (!synchronizationContextBefore.Equals(synchronizationContextAfter)) && 
                   (managedThreadIdAfterBefore != managedThreadIdAfter);
        }
    }
}
