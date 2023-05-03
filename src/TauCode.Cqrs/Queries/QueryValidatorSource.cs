using FluentValidation;
using System.Reflection;

namespace TauCode.Cqrs.Queries;

public class QueryValidatorSource : IQueryValidatorSource
{
    public QueryValidatorSource(params Assembly[] assemblies)
    {
        throw new NotImplementedException();
    }

    public Type? GetQueryValidatorType(Type queryType)
    {
        throw new NotImplementedException();
    }

    public AbstractValidator<TQuery>? CreateQueryValidator<TQuery>() where TQuery : IQuery
    {
        throw new NotImplementedException();
    }
}