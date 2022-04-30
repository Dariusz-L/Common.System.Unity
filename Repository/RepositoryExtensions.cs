using Common.Basic.Blocks;
using System;
using System.Threading.Tasks;

namespace Common.Basic.Repository
{
    public static class RepositoryExtensions
    {
        public static async Task<T> GetEntity<T>(this IRepository<T> repository, string id)
        {
            var res = await repository.GetBy(id);
            return res.Get<T>();
        }

        public static async Task<bool> SaveEntity<T>(this IRepository<T> repository, T item)
        {
            var res = await repository.Save(item);
            return res.IsSuccess;
        }

        public static async Task<Result> GetRunAndSaveEntity<T>(this IRepository<T> repository, string id, Func<T, bool> operation)
        {
            var entity = await repository.GetEntity(id);
            if (!operation(entity))
                return Result.Failure();

            return repository.Save(entity);
        }

        public static async Task<Result<T>> CreateNewAndSaveEntityIfNotExistsOfName<T>(
            this IRepository<T> repository, string name, Func<T, string> getName)
        {
            var result = new Result<T>();

            var existsResult = await repository.ExistsOfName(name, getName);
            if (!existsResult.IsSuccess)
                return result.With(existsResult);

            bool exists = existsResult.Get<bool>();
            if (exists)
                return result;

            string id = Guid.NewGuid().ToString();
            T entity = (T) Activator.CreateInstance(typeof(T), new object[] { id, name });

            var saveResult = await repository.Save(entity);
            if (!saveResult.IsSuccess)
                return result.With(saveResult);

            return result.With(entity);
        }

        public static async Task<Result<T>> GetIfExistsOrCreate<T>(this IRepository<T> repository, string id, Func<T> createEntity)
        {
            var result = await repository.GetBy(id);
            if (!result.IsSuccess)
                return result;

            T entity = result.Get<T>();
            if (entity != null)
                return result;

            return createEntity().ToResult();
        }

        public static async Task<Result<T>> GetIfExistsOrCreateAndSave<T>(this IRepository<T> repository, string id, Func<T> createEntity)
        {
            var result = await repository.GetBy(id);
            if (!result.IsSuccess)
                return result;

            T entity = result.Get<T>();
            if (entity != null)
                return result;

            entity = createEntity();
            var saveResult = await repository.Save(entity);
            if (!saveResult.IsSuccess)
                return result.With(saveResult);

            return result.With(entity);
        }

        public static Result GetIfExistsOrCreateAndSaveA<T>(this IRepository<T> repository, string id, Func<T> createEntity)
        {
            return null;
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
