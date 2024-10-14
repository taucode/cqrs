namespace TauCode.Cqrs.Commands;

public interface ICommandHandler<in TCommand> : ICommandHandler
    where TCommand : ICommand
{
    void Execute(TCommand command);

    Task ExecuteAsync(TCommand command, CancellationToken cancellationToken);
}