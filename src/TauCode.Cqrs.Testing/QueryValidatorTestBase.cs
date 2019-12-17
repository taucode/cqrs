using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;
using System;
using TauCode.Cqrs.Queries;

namespace TauCode.Cqrs.Testing
{
    [TestFixture]
    public abstract class QueryValidatorTestBase<TQuery, TValidator>
        where TQuery : IQuery
        where TValidator : AbstractValidator<TQuery>
    {
        protected TValidator Validator { get; set; }

        [SetUp]
        public void SetUpBase()
        {
            this.Validator = this.CreateValidator();
        }

        protected ValidationResult ValidateQuery(TQuery query)
        {
            var validationResult = this.Validator.Validate(query);
            return validationResult;
        }

        protected virtual TValidator CreateValidator()
        {
            var ctor = typeof(TValidator).GetConstructor(Type.EmptyTypes);
            if (ctor == null)
            {
                throw new InvalidOperationException($"No parameterless constructor defined for type '{typeof(TValidator).FullName}'.");
            }

            var validator = (TValidator) ctor.Invoke(new object[0]);
            return validator;
        }

        protected abstract TQuery CreateQuery();
    }
}
