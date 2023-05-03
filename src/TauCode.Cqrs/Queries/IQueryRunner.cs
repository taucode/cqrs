namespace TauCode.Cqrs.Queries;

public interface IQueryRunner
{
    void Run(IQuery query);

    void Run<TQuery>(TQuery query) where TQuery : IQuery;

    Task RunAsync(IQuery query, CancellationToken cancellationToken = default);

    Task RunAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery;
}