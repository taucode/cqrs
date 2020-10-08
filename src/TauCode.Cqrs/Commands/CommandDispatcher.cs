using System;
using System.Threading;
using System.Threading.Tasks;
using TauCode.Cqrs.Abstractions;
using TauCode.Cqrs.Exceptions;

namespace TauCode.Cqrs.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        #region Fields

        protected readonly ICommandHandlerFactory CommandHandlerFactory;

        #endregion

        #region Constructor

        public CommandDispatcher(ICommandHandlerFactory commandHandlerFactory)
        {
            CommandHandlerFactory =
                commandHandlerFactory ?? throw new ArgumentNullException(nameof(commandHandlerFactory));
        }

        #endregion

        #region Virtual

        protected virtual void OnBeforeExecuteHandler<TCommand>(ICommandHandler<TCommand> handler, TCommand command)
            where TCommand : ICommand
        {
            // idle, override in ancestor if needed.
        }

        protected virtual Task OnBeforeExecuteHandlerAsync<TCommand>(
            ICommandHandler<TCommand> handler,
            TCommand command,
            CancellationToken cancellationToken)
            where TCommand : ICommand
        {
            // idle, override in ancestor if needed.
            return Task.CompletedTask;
        }

        #endregion

        #region ICommandDispatcher Members

        public void Dispatch<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            ICommandHandler<TCommand> commandHandler;

            try
            {
                commandHandler = CommandHandlerFactory.Create<TCommand>();
            }
            catch (Exception ex)
            {
                throw new CqrsException($"Failed to create command handler for command of type '{typeof(TCommand).FullName}'.", ex);
            }

            if (commandHandler == null)
            {
                throw new CqrsException($"'{nameof(CommandHandlerFactory)}.{nameof(ICommandHandlerFactory.Create)}' returned 'null'.");
            }

            this.OnBeforeExecuteHandler(commandHandler, command);

            commandHandler.Execute(command);
        }

        public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            ICommandHandler<TCommand> commandHandler;
            try
            {
                commandHandler = CommandHandlerFactory.Create<TCommand>();
            }
            catch (Exception ex)
            {
                throw new CqrsException($"Failed to create command handler for command of type '{typeof(TCommand).FullName}'.", ex);
            }

            if (commandHandler == null)
            {
                throw new CqrsException($"'{nameof(CommandHandlerFactory)}.{nameof(ICommandHandlerFactory.Create)}' returned 'null'.");
            }

            await this.OnBeforeExecuteHandlerAsync(commandHandler, command, cancellationToken);
            await commandHandler.ExecuteAsync(command, cancellationToken);
        }

        #endregion
    }
}
