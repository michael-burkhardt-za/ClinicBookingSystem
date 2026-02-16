using FluentValidation;
using Domain;

namespace Application.Validators
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(x => x.Id)
                .Equal(0)
                .WithMessage("Id be must equal to zero");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("First Name must be valid");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Last name must be valid");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Email must be valid");
            // POST rules
            RuleSet("Create", () =>
            {
                RuleFor(x => x.Id)
                    .Equal(0)
                    .WithMessage("Id must be 0 when creating.");
            });

            // PUT rules
            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                    .WithMessage("Id must be greater than 0 when updating.");
            });
        }
    }
}
