using FluentValidation;

namespace TauCode.Cqrs.Queries;

public interface IQueryValidatorSource
{
    Type? GetQueryValidatorType(Type queryType);

    AbstractValidator<TQuery>? CreateQueryValidator<TQuery>() where TQuery : IQuery;
}