namespace TauCode.Cqrs.Commands;

public interface ICommandHandler
{
    void Execute(ICommand command);

    Task ExecuteAsync(ICommand command, CancellationToken cancellationToken);
}