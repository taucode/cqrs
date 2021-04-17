using System.Threading;
using System.Threading.Tasks;
using TauCode.Cqrs.Abstractions;

namespace TauCode.Cqrs.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command) where TCommand : ICommand;

        Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;
    }
}
