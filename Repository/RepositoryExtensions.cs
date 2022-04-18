using Common.Basic.Blocks;
using System;
using System.Threading.Tasks;

namespace Common.Basic.Repository
{
    public static class RepositoryExtensions
    {
        public static Task<T> GetEntity<T>(this IRepository<T> repository, string id)
        {
            return Task.Run(
                async () =>
                {
                    var res = await repository.GetBy(id);
                    return res.Get<T>();
                });
        }

        public static Task SaveEntity<T>(this IRepository<T> repository, T item)
        {
            return Task.Run(async () =>
            {
                var result = await repository.Save(item);
                return result;
            });
        }

        public static async Task<Result> GetRunAndSaveEntity<T>(this IRepository<T> repository, string id, Func<T, bool> operation)
        {
            var entity = await repository.GetEntity(id);
            if (!operation(entity))
                return Result.Failure();

            return repository.Save(entity);
        }

        public static async Task<Result> GetByNameCreateAndSaveEntity<T>(
            this IRepository<T> repository, string name, Func<string, Task<bool>> existsOfName)
        {
            bool exists = await existsOfName(name);
            if (exists)
                return Result.Failure();

            string id = Guid.NewGuid().ToString();
            T entity = (T) Activator.CreateInstance(typeof(T), new object[] { id, name });

            return repository.Save(entity);
        }

        public static async Task<Result> GetIfExistsOrCreateAndSave<T>(this IRepository<T> repository, string id, Func<T> createEntity)
        {
            var result = await repository.GetBy(id);
            if (!result.IsSuccess)
                return result;

            T entity = result.Get<T>();
            if (entity != null)
                return result;

            entity = createEntity();
            return repository.Save(entity);
        }

        public static async Task<Result<string>> GetRunAndSaveEntity_ThenCreateNew<T>(
            this IRepository<T> repository, string id, Func<T, string, bool> operation, Func<string, T> create)
        {
            var entity = await repository.GetEntity(id);

            string newID = Guid.NewGuid().ToString();
            if (!operation(entity, newID))
                return Result<string>.Failure();

            var newEntity = create(newID);
            if (newEntity == null)
                return Result<string>.Failure();

            var saveNewResult = await repository.Save(newEntity);
            if (!saveNewResult.IsSuccess)
                return new Result<string>(saveNewResult);

            var saveOldResult = await repository.Save(entity);
            if (!saveOldResult.IsSuccess)
                return new Result<string>(saveOldResult);

            return Result<string>.Success();
        }
    }
}
