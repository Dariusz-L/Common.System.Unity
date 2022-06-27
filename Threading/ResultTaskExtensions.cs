using Common.Basic.Blocks;
using System.Threading.Tasks;

namespace Common.Basic.Threading
{
    public static class ResultTaskExtensions
    {
        public static T Value<T>(this Task<Result<T>> task, bool configureAwait = false)
        {
            return task.ConfigureAwait(configureAwait).GetAwaiter().GetResult().Get();
        }
    }
}
