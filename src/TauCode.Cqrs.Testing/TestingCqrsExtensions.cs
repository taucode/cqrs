using FluentValidation.Results;
using NUnit.Framework;
using TauCode.Domain.Identities;

namespace TauCode.Cqrs.Testing
{
    public static class TestingCqrsExtensions
    {
        public static ValidationResult ShouldBeValid(this ValidationResult validationResult)
        {
            Assert.That(validationResult.IsValid, Is.True);
            return validationResult;
        }

        public static ValidationResult ShouldBeInvalid(this ValidationResult validationResult, int expectedErrorCount)
        {
            Assert.That(validationResult.IsValid, Is.False);
            Assert.That(validationResult.Errors, Has.Count.EqualTo(expectedErrorCount));

            return validationResult;
        }

        public static ValidationResult ShouldHaveError(
            this ValidationResult validationResult,
            int errorIndex,
            string propertyName,
            string expectedErrorCode,
            string expectedErrorMessage)
        {
            Assert.That(validationResult.Errors[errorIndex].PropertyName, Is.EqualTo(propertyName));
            Assert.That(validationResult.Errors[errorIndex].ErrorCode, Is.EqualTo(expectedErrorCode));
            Assert.That(validationResult.Errors[errorIndex].ErrorMessage, Is.EqualTo(expectedErrorMessage));

            return validationResult;
        }

        public static TId ToId<TId>(this string id) where TId : IdBase
        {
            if (id == null)
            {
                return null;
            }

            return (TId)typeof(TId)
                .GetConstructor(new[] { typeof(string) })
                .Invoke(new object[] { id });
        }
    }
}
