using Common.Basic.Blocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Basic.Repository
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get resource by identification string.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Success result with given type of resource. If none exists then result is also a success, abut with no given resource. Otherwise false.</returns>
        Task<Result<T>> GetBy(string id);
        Task<Result<IEnumerable<T>>> GetAll();
        Task<Result> Save(T item);
        Task<Result> Clear();
        Task<Result> Delete(string id);
    }
}
