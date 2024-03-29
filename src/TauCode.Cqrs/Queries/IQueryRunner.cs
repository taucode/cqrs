﻿namespace TauCode.Cqrs.Queries;

public interface IQueryRunner
{
    void Run<TQuery>(TQuery query) where TQuery : IQuery;

    Task RunAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery;
}