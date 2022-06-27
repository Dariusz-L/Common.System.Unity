using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.DDD;
using Common.Basic.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Basic.Repository
{
    public class CachedInMemoryRepositoryDecorator<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _repository;
        private List<TEntity> _cached;

        public CachedInMemoryRepositoryDecorator(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        Task<Result> IRepository<TEntity>.Clear()
        {
            throw new NotImplementedException();
        }

        async Task<Result> IRepository<TEntity>.Delete(string id)
        {
            InitIfNotYet();

            var result = await _repository.Delete(id);
            if (!result.IsSuccess)
                return result;

            _cached.RemoveFirstIf(e => e.ID == id);
            return Result.Success();
        }

        async Task<Result<bool>> IRepository<TEntity>.ExistsOfName(string name, Func<TEntity, string> getName)
        {
            InitIfNotYet();

            bool exists = _cached.FirstOrDefault(e => getName(e) == name) != null;
            return Result.SuccessTask(exists);
        }

        Task<Result<TEntity[]>> IRepository<TEntity>.GetAll()
        {
            InitIfNotYet();
            return Result<TEntity[]>.SuccessTask(_cached.ToArray());
        }

        Task<Result<TEntity>> IRepository<TEntity>.GetBy(string id)
        {
            InitIfNotYet();

            var entity = _cached.FirstOrDefault(e => e.ID == id);
            return Result<TEntity>.SuccessTask(entity);
        }

        async Task<Result> IRepository<TEntity>.Save(TEntity item)
        {
            InitIfNotYet();

            var result = await _repository.Save(item);
            if (!result.IsSuccess)
                return result;

            var entity = _cached.FirstOrDefault(e => e.ID == item.ID);
            if (entity != null)
                _cached.Remove(entity);

            _cached.Add(item);
            return Result.Success();
        }

        private void InitIfNotYet()
        {
            if (_cached != null)
                return;

            _cached = _repository.GetAll().Value().ToList();
        }
    }
}
