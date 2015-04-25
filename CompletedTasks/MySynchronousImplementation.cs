using CompletedTasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompletedTasks
{
    public class MySynchronousImplementation : IMyAsyncInterface
    {
        private static readonly Task<int> completedZeroTask = Task.FromResult(0);

        public static Task<int> GetZeroValueAsync()
        {
            return completedZeroTask;
        }

        public Task<int> GetValueAsync()
        {
            return Task.FromResult(13);
        }

        public static Task<T> NotImplementatedAsync<T>()
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetException(new NotImplementedException());
            return tcs.Task;
        }
    }
}
