using Common.Basic.Blocks;
using System.Threading.Tasks;

namespace Common.Basic.CQRS.Query
{
    public interface IQueryExecutor
    {
        Task<Result<TQueryOutput>> Execute<TQuery, TQueryOutput>(TQuery query)
           where TQuery : IQuery
           where TQueryOutput : IQueryOutput;

        Task<Result<TQueryOutput>> Execute<TQuery, TQueryOutput>()
           where TQuery : IQuery, new()
           where TQueryOutput : IQueryOutput;
    }
}
