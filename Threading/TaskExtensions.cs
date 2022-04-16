using System.Threading.Tasks;

namespace Common.Basic.Threading
{
    public static class TaskExtensions
    {
        public static T GetAwaiterResult<T>(this Task<T> task)
        {
            return task.GetAwaiter().GetResult();
        }
    }
}
