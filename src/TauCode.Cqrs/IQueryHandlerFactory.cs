namespace TauCode.Cqrs;

public interface IQueryHandlerFactory
{
    IQueryHandler<TQuery> Create<TQuery>() where TQuery : IQuery;
}