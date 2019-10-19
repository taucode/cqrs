using System.Threading;
using System.Threading.Tasks;

namespace TauCode.Cqrs.Commands
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public virtual void Execute(TCommand command)
        {
            throw new FeatureNotImplementedException();
        }

        public virtual Task ExecuteAsync(TCommand command, CancellationToken cancellationToken)
        {
            throw new FeatureNotImplementedException();
        }
    }
}
