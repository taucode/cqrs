﻿namespace TauCode.Cqrs.Queries;

public interface IQueryHandler<in TQuery> : IQueryHandler
    where TQuery : IQuery
{
    void Execute(TQuery query);

    Task ExecuteAsync(TQuery query, CancellationToken cancellationToken);
}