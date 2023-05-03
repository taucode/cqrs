using Microsoft.Extensions.DependencyInjection;

namespace TauCode.Cqrs.Queries;

// todo regions
public class QueryRunner : IQueryRunner
{
    protected IServiceProvider ServiceProvider { get; }

    public QueryRunner(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }

    public virtual void Run(IQuery query)
    {
        throw new NotSupportedException();
    }

    public void Run<TQuery>(TQuery query) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    protected virtual Task OnBeforeExecuteAsync(
        IQueryHandler queryHandler,
        IQuery query,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnAfterExecuteAsync(
        IQueryHandler queryHandler,
        IQuery query,
        Exception? exception,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public virtual async Task RunAsync(IQuery query, CancellationToken cancellationToken = default)
    {
        // todo: dynamically invoke RunAsync<TCommand>?

        var queryType = query.GetType();
        var queryHandlerType = typeof(IQueryHandler<>).MakeGenericType(queryType);
        var queryHandler = (IQueryHandler)this.ServiceProvider.GetRequiredService(queryHandlerType);

        await this.OnBeforeExecuteAsync(queryHandler, query, cancellationToken);

        try
        {
            await queryHandler.ExecuteAsync(query, cancellationToken);
        }
        catch (Exception ex)
        {
            await this.OnAfterExecuteAsync(queryHandler, query, ex, cancellationToken);
            throw;
        }

        await this.OnAfterExecuteAsync(queryHandler, query, null, cancellationToken);
    }

    public async Task RunAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery
    {
        var queryHandler = this.ServiceProvider.GetRequiredService<IQueryHandler<TQuery>>();

        await this.OnBeforeExecuteAsync(queryHandler, query, cancellationToken);

        try
        {
            await queryHandler.ExecuteAsync(query, cancellationToken);
        }
        catch (Exception ex)
        {
            await this.OnAfterExecuteAsync(queryHandler, query, ex, cancellationToken);
            throw;
        }

        await this.OnAfterExecuteAsync(queryHandler, query, null, cancellationToken);
    }
}