namespace TauCode.Cqrs.Queries
{
    public abstract class SyncQueryHandlerBase<TQuery> : IQueryHandler<TQuery> where TQuery : IQuery
    {
        public abstract void Execute(TQuery query);

        public Task ExecuteAsync(TQuery query, CancellationToken cancellationToken)
        {
            throw new NotSupportedException($"Use sync overload ('{nameof(Execute)}') of '{this.GetType().FullName}'.");
        }
    }
}
