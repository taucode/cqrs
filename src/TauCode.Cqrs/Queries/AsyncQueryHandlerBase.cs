namespace TauCode.Cqrs.Queries;

public abstract class AsyncQueryHandlerBase<TQuery> : IQueryHandler<TQuery> where TQuery : IQuery
{
    public void Execute(TQuery query)
    {
        throw new NotSupportedException($"Use async overload ('{nameof(ExecuteAsync)}') of '{this.GetType().FullName}'.");
    }

    public abstract Task ExecuteAsync(TQuery query, CancellationToken cancellationToken);
}