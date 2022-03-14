using Common.Basic.Blocks;
using System.Threading.Tasks;

namespace Common.App.CQRS.Command
{
    public interface ICommandExecutor
    {
        Task<Result> Execute<TCommand>(TCommand command)
            where TCommand : ICommand;

        Task<Result<TCommandOutput>> Execute<TCommand, TCommandOutput>(TCommand command)
            where TCommand : ICommand;
    }
}
