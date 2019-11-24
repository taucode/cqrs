using Autofac;
using TauCode.Cqrs.Commands;

namespace TauCode.Cqrs.Autofac
{
    public class AutofacCommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacCommandHandlerFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public ICommandHandler<TCommand> Create<TCommand>() where TCommand : ICommand
        {
            return _lifetimeScope.Resolve<ICommandHandler<TCommand>>();
        }
    }
}
