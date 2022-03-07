using Common.Basic.Blocks;
using Common.Basic.DDD;
using Common.Basic.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Basic.Repository
{
    public class LocalStorageRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly string _pathToDirectory;
        private readonly IJsonConverter _jsonConverter;

        public LocalStorageRepository(string pathToDirectory, IJsonConverter jsonConverter)
        {
            _pathToDirectory = pathToDirectory;
            _jsonConverter = jsonConverter;
        }

        private static string CreateFilePath(string pathToDirectory, string id)
        {
            return pathToDirectory.Trim('\\', '/') + '/' + id;
        }

        Task<Result<TEntity>> IRepository<TEntity>.GetBy(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Result<TEntity>.FailureTask();

            if (!Directory.Exists(_pathToDirectory))
                Directory.CreateDirectory(_pathToDirectory);

            if (!Directory.Exists(_pathToDirectory))
                return Result<TEntity>.FailureTask();

            string filePath = CreateFilePath(_pathToDirectory, id);

            try
            {
                var fileText = File.ReadAllText(filePath);
                var obj = _jsonConverter.Deserialize<TEntity>(fileText);
                return Result<TEntity>.SuccessTask(obj);
            }
            catch (Exception ex)
            {
                return Result<TEntity>.FailureTask(ex);
            }
        }

        Task<Result<IEnumerable<TEntity>>> IRepository<TEntity>.GetAll()
        {
            if (!Directory.Exists(_pathToDirectory))
                Directory.CreateDirectory(_pathToDirectory);

            string[] filePaths = Directory.GetFiles(_pathToDirectory);
            if (filePaths == null || filePaths.Length == 0)
                return Array.Empty<TEntity>().ToResultTask<IEnumerable<TEntity>>();

            var fileTexts = filePaths.Select(fp => File.ReadAllText(fp));
            var entities = fileTexts.Select(ft => _jsonConverter.Deserialize<TEntity>(ft));

            return Result<IEnumerable<TEntity>>.SuccessTask(entities);
        }

        Task<Result> IRepository<TEntity>.Save(TEntity item)
        {
            if (string.IsNullOrWhiteSpace(item.ID))
                return Result.FailureTask();

            if (item == null)
                return Result.FailureTask();

            if (!Directory.Exists(_pathToDirectory))
                Directory.CreateDirectory(_pathToDirectory);

            string filePath = CreateFilePath(_pathToDirectory, item.ID);

            item.Version++;
            var fileData = _jsonConverter.Serialize(item);

            try
            {
                File.WriteAllText(filePath, fileData);
                return Result.SuccessTask();
            }
            catch (Exception ex)
            {
                return Result.FailureTask(ex);
            }
        }

        Task<Result> IRepository<TEntity>.Clear()
        {
            throw new NotImplementedException();
        }

        Task<Result> IRepository<TEntity>.Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Result.FailureTask();

            if (!Directory.Exists(_pathToDirectory))
                Directory.CreateDirectory(_pathToDirectory);

            string filePath = CreateFilePath(_pathToDirectory, id);
            try
            {
                File.Delete(_pathToDirectory);
                return Result.SuccessTask();
            }
            catch (Exception ex)
            {
                return Result.FailureTask(ex);
            }
        }
    }
}
