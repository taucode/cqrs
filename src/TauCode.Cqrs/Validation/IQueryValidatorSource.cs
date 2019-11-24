using FluentValidation;
using System;
using TauCode.Cqrs.Queries;

namespace TauCode.Cqrs.Validation
{
    public interface IQueryValidatorSource
    {
        Type[] GetQueryTypes();
        IValidator<TQuery> GetValidator<TQuery>() where TQuery : IQuery;
    }
}
