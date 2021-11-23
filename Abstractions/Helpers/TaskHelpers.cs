using System;
using System.Threading.Tasks;

namespace Filuet.Infrastructure.Abstractions.Helpers
{
    public static class TaskHelpers
    {
        public static bool ExecuteWithTimeLimit(TimeSpan timeSpan, Action action)
        {
            try
            {
                Task task = Task.Factory.StartNew(() => action());
                task.Wait(timeSpan);
                return task.IsCompleted;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerExceptions[0];
            }
        }
    }
}