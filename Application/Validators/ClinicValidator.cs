using FluentValidation;
using Domain;

namespace Application.Validators
{
    public class ClinicValidator : AbstractValidator<Clinic>
    {
        public ClinicValidator()
        {
            RuleFor(x => x.Id)
                   .Equal(0)
                   .WithMessage("Id be must equal to zero");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Clinic Name must be valid");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Phone must be valid");

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Address must be valid");

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
