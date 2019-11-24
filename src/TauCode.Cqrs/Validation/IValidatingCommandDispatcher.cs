using TauCode.Cqrs.Commands;

namespace TauCode.Cqrs.Validation
{
    public interface IValidatingCommandDispatcher : ICommandDispatcher
    {
        void Validate<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
