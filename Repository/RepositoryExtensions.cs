using Common.Basic.Blocks;
using System;
using System.Threading.Tasks;

namespace Common.Basic.Repository
{
    public static class RepositoryExtensions
    {
        public static Task<T> GetEntity<T>(this IRepository<T> repository, string id)
        {
            return Task.Run(() =>
            {
                var res = repository.GetBy(id).GetAwaiter().GetResult();
                return res.Get<T>();
            });
        }

        public static Task SaveEntity<T>(this IRepository<T> repository, T item)
        {
            return Task.Run(() =>
            {
                return repository.Save(item).GetAwaiter().GetResult();
            });
        }

        public static async Task<Result> GetRunSaveEntity<T>(this IRepository<T> repository, string id, Func<T, bool> function)
        {
            var entity = await repository.GetEntity(id);
            if (!function(entity))
                return Result.Failure();

            return repository.Save(entity);
        }
    }
}
