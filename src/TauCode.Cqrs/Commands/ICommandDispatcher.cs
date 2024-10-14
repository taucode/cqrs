namespace TauCode.Cqrs.Commands;

public interface ICommandDispatcher
{
    void Dispatch(ICommand command);

    void Dispatch<TCommand>(TCommand command) where TCommand : ICommand;

    Task DispatchAsync(ICommand command, CancellationToken cancellationToken = default);

    Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;
}