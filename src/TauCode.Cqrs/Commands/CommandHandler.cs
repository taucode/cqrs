namespace TauCode.Cqrs.Commands;

// todo regions
public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public virtual void Execute(TCommand command)
    {
        throw new NotSupportedException(); // todo
    }

    public abstract Task ExecuteAsync(TCommand command, CancellationToken cancellationToken);

    public virtual void Execute(ICommand command)
    {
        this.Execute((TCommand)command);
    }

    public virtual Task ExecuteAsync(ICommand command, CancellationToken cancellationToken)
    {
        return this.ExecuteAsync((TCommand)command, cancellationToken);
    }
}