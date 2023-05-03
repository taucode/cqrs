using Microsoft.Extensions.DependencyInjection;

namespace TauCode.Cqrs.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    protected IServiceProvider ServiceProvider { get; }

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }

    public virtual void Dispatch(ICommand command)
    {
        throw new NotSupportedException();
    }

    public void Dispatch<TCommand>(TCommand command) where TCommand : ICommand
    {
        throw new NotImplementedException();
    }

    protected virtual Task OnBeforeExecuteAsync(
        ICommandHandler commandHandler,
        ICommand command,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnAfterExecuteAsync(
        ICommandHandler commandHandler,
        ICommand command,
        Exception? exception,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public virtual async Task DispatchAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        // todo: dynamically invoke DispatchAsync<TCommand>?
        var commandType = command.GetType();
        var commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
        var commandHandler = (ICommandHandler)this.ServiceProvider.GetRequiredService(commandHandlerType);

        await this.OnBeforeExecuteAsync(commandHandler, command, cancellationToken);

        try
        {
            await commandHandler.ExecuteAsync(command, cancellationToken);
        }
        catch (Exception ex)
        {
            await this.OnAfterExecuteAsync(commandHandler, command, ex, cancellationToken);
            throw;
        }

        await this.OnAfterExecuteAsync(commandHandler, command, null, cancellationToken);
    }

    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        var commandHandler = this.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        await this.OnBeforeExecuteAsync(commandHandler, command, cancellationToken);

        try
        {
            await commandHandler.ExecuteAsync(command, cancellationToken);
        }
        catch (Exception ex)
        {
            await this.OnAfterExecuteAsync(commandHandler, command, ex, cancellationToken);
            throw;
        }

        await this.OnAfterExecuteAsync(commandHandler, command, null, cancellationToken);
    }
}