using System;
using FluentValidation;

namespace WebApp.Models.Validations
{
    public class AppointmentValidator : AbstractValidator<AppointmentModel>
    {
        public AppointmentValidator()
        {
            RuleFor(c => c.AppointmentDate).NotNull().WithMessage("Trebuie sa specificati o data").Must(BeAValidDate);
            RuleFor(c => c.Patient).NotNull();
            RuleFor(c => c.Doctor).NotNull();
        }

        private bool BeAValidDate(DateTime date)
        {
            return date>DateTime.Now;
        }
    }
}
