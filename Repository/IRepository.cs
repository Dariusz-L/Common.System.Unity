using Common.Basic.Blocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Basic.Repository
{
    public interface IRepository<T>
    {
        Task<Result<T>> GetBy(string id);
        Task<Result<IEnumerable<T>>> GetAll();
        Task<Result> Save(T item);
        Task<Result> Clear();
        Task<Result> Delete(string id);
    }
}
