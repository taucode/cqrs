using System.Threading;
using System.Threading.Tasks;

namespace TauCode.Cqrs.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);

        Task ExecuteAsync(TCommand command, CancellationToken cancellationToken);
    }
}
