namespace TauCode.Cqrs;

public interface ICommandHandlerFactory
{
    ICommandHandler<TCommand> Create<TCommand>() where TCommand : ICommand;
}