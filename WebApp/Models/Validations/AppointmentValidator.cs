using System;
using FluentValidation;

namespace WebApp.Models.Validations
{
    public class AppointmentValidator : AbstractValidator<AppointmentModel>
    {
        public AppointmentValidator()
        {
            RuleFor(c => c.AppointmentDate).NotNull().WithMessage("Trebuie sa specificati o data").Must(BeAValidDate);
            RuleFor(c => c.PatientId).NotNull();
            RuleFor(c => c.DoctorId).NotNull();
        }

        private bool BeAValidDate(DateTime date)
        {
            return date>DateTime.Now;
        }
    }
}
