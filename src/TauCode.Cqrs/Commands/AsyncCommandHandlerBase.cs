namespace TauCode.Cqrs.Commands
{
    public abstract class AsyncCommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public void Execute(TCommand command)
        {
            throw new NotSupportedException($"Use async overload ('{nameof(ExecuteAsync)}') of '{this.GetType().FullName}'.");
        }

        public abstract Task ExecuteAsync(TCommand command, CancellationToken cancellationToken);
    }
}
