using Autofac;
using TauCode.Cqrs.Queries;

namespace TauCode.Cqrs.Autofac
{
    public class AutofacQueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacQueryHandlerFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public IQueryHandler<TQuery> Create<TQuery>() where TQuery : IQuery
        {
            return _lifetimeScope.Resolve<IQueryHandler<TQuery>>();
        }
    }
}
