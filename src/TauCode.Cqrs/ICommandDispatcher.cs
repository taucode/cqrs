namespace TauCode.Cqrs;

public interface ICommandDispatcher
{
    void Dispatch<TCommand>(TCommand command) where TCommand : ICommand;

    Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand;
}