using System;
using FluentValidation;

namespace WebApp.Models.Validations
{
    public class PatientHistoryValidator : AbstractValidator<PatientHistoryModel>
    {
        public PatientHistoryValidator()
        {
            RuleFor(c => c.Prescription).NotNull().WithMessage("Trebuie sa specificati o retea").Length(3, 250)
                .WithMessage("Trebuie sa aiba minim 3 caractere si maxim 250");
            RuleFor(c => c.Description).NotNull().WithMessage("Trebuie sa specificati o descriere").Length(3,250)
                .WithMessage("Trebuie sa aiba minim 3 caractere si maxim 250");
            RuleFor(c => c.Recommendations).NotNull().WithMessage("Trebuie sa specificati recomandari").Length(3, 250)
                .WithMessage("Trebuie sa aiba minim 3 caractere si maxim 250");
            RuleFor(c => c.Date).NotNull().WithMessage("Trebuie sa specificati data de nastere").Must(BeAValidDate);
            RuleFor(c => c.DoctorId).NotNull();
            RuleFor(c => c.PatientId).NotNull();
        }
        private bool BeAValidDate(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
