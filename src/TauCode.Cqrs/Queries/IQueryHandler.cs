namespace TauCode.Cqrs.Queries;

public interface IQueryHandler
{
    void Execute(IQuery query);

    Task ExecuteAsync(IQuery query, CancellationToken cancellationToken);
}