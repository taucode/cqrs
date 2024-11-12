namespace TauCode.Cqrs;

public interface IQueryHandler<in TQuery>
    where TQuery : IQuery
{
    void Execute(TQuery query);

    Task ExecuteAsync(TQuery query, CancellationToken cancellationToken = default);
}