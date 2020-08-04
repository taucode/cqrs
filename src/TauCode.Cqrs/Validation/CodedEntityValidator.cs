using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using TauCode.Cqrs.Queries;
using TauCode.Validation;

namespace TauCode.Cqrs.Validation
{
    public class CodedEntityValidator<TCodedEntityQuery, TCodeValidator> : AbstractValidator<TCodedEntityQuery>
        where TCodedEntityQuery : ICodedEntityQuery
        where TCodeValidator : CodeValidator, new()
    {
        public CodedEntityValidator()
        {
            this.RuleFor(x => x.GetCode())
                .SetValidator(new TCodeValidator())
                .WithName(x => x.GetCodePropertyName());

            this.RuleFor(x => x)
                .Custom(PropertiesCheck);
        }

        private void PropertiesCheck(TCodedEntityQuery query, CustomContext customContext)
        {
            var oneIsValid = query.GetId() == null ^ query.GetCode() == null;
            if (!oneIsValid)
            {
// todo: not 'Code', but '<GetCodePropertyName>'!
                customContext.AddFailure(new ValidationFailure("query", "Either 'Id' or 'Code' must be not null.")
                {
                    ErrorCode = "CodedEntityQueryValidator",
                });
            }
        }
    }
}
