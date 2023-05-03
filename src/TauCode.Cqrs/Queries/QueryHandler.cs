namespace TauCode.Cqrs.Queries;

// todo regions
public abstract class QueryHandler<TQuery> : IQueryHandler<TQuery>
    where TQuery : IQuery
{
    public virtual void Execute(TQuery query)
    {
        throw new NotSupportedException(); // todo
    }

    public abstract Task ExecuteAsync(TQuery query, CancellationToken cancellationToken);

    public void Execute(IQuery query)
    {
        this.Execute((TQuery)query);
    }

    public Task ExecuteAsync(IQuery query, CancellationToken cancellationToken)
    {
        return this.ExecuteAsync((TQuery)query, cancellationToken);
    }
}