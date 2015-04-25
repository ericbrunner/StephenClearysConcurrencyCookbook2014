using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompletedTasks.Interfaces
{
    interface IMyAsyncInterface
    {
        Task<int> GetValueAsync();
    }
}
