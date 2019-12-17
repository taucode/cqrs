using System;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;
using TauCode.Cqrs.Commands;

namespace TauCode.Cqrs.Testing
{
    [TestFixture]
    public abstract class CommandValidatorTestBase<TCommand, TValidator>
        where TCommand : ICommand
        where TValidator : AbstractValidator<TCommand>
    {
        protected TValidator Validator { get; set; }

        [SetUp]
        public void SetUpBase()
        {
            this.Validator = this.CreateValidator();
        }

        protected ValidationResult ValidateCommand(TCommand command)
        {
            var validationResult = this.Validator.Validate(command);
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

        protected abstract TCommand CreateCommand();
    }
}
