using System.Threading.Tasks;

namespace Common.Basic.Threading
{
    public static class TaskExtensions
    {
        public static T GetAwaiterResult<T>(this Task<T> task)
        {
            return task.GetAwaiter().GetResult();
        }

        public static T Result<T>(this Task<T> task, bool configureAwait = false)
        {
            return task.ConfigureAwait(configureAwait).GetAwaiter().GetResult();
        }
    }
}
