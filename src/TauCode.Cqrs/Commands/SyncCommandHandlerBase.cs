﻿using System;
using System.Threading;
using System.Threading.Tasks;
using TauCode.Cqrs.Abstractions;

namespace TauCode.Cqrs.Commands
{
    public abstract class SyncCommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public abstract void Execute(TCommand command);

        public Task ExecuteAsync(TCommand command, CancellationToken cancellationToken)
        {
            throw new NotSupportedException($"Use sync overload ('{nameof(Execute)}') of '{this.GetType().FullName}'.");
        }
    }
}
