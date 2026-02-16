using FluentValidation;
using Domain;
 

namespace Application.Validators
{
    public class AppointmentBookingValidator : AbstractValidator<AppointmentBooking>
    {
        public AppointmentBookingValidator()
        {
            RuleFor(x => x.ClinicId)
                .GreaterThan(0)
                .WithMessage("ClinicId must be greater than zero.");

            RuleFor(x => x.PatientId)
                .GreaterThan(0)
                .WithMessage("PatientId must be greater than zero.");

            RuleFor(x => x.AppointmentDate)
             .Must(date => date > DateTime.UtcNow.AddMinutes(5))
             .WithMessage("Appointment must in the future.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(50);

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
