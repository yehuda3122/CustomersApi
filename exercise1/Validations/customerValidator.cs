using FluentValidation;

namespace exercise1.Validations
{
    public class customerValidator : AbstractValidator<CustomerDto>
    {
        public customerValidator()
        {
            RuleFor(x => x.GuId).NotEmpty()
                .WithMessage("GuId cannot be empty");
            RuleFor(x => x.Name).NotEmpty()
                .NotNull()
                .Length(3,300)
                .WithMessage("name must be between 3 to 300 leters");
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Email cannot be null")
                .EmailAddress()
                .WithMessage("Email is not valid");
        }
    }
}
