namespace TauCode.Cqrs.Queries;

public interface IQueryHandlerFactory
{
    IQueryHandler<TQuery> Create<TQuery>() where TQuery : IQuery;
}