using System;
using FluentValidation;

namespace WebApp.Models.Validations
{
    public class UpdatingPatientValidator : AbstractValidator<UpdatePatientModel>
    {
        public UpdatingPatientValidator()
        {
            RuleFor(c => c.FirstName).NotNull().WithMessage("Trebuie sa specificati un prenume").Length(3, 30)
                .WithMessage("Trebuie sa aiba intre 3 si 30 caractere");
            RuleFor(c => c.LastName).NotNull().WithMessage("Trebuie sa specificati un nume").Length(3, 30)
                .WithMessage("Trebuie sa aiba intre 3 si 30 caractere");
            RuleFor(c => c.Email).NotNull().WithMessage("Trebuie sa specificati o adresa de email").EmailAddress()
                .WithMessage("Trebuie sa specificati o adresa valida");
            RuleFor(c => c.Password).NotNull().WithMessage("Trebuie sa specificati o parola").Length(6, 20)
                .WithMessage("Parola trebuie sa fie intre 6 si 20 caractere");
            RuleFor(c => c.City).NotNull().WithMessage("Trebuie sa specificati un oras").Length(3, 30)
                .WithMessage("Numele orasului trebuie sa fie intre 3 si 30 caracterE");
            RuleFor(c => c.Birthdate).NotNull().WithMessage("Trebuie sa specificati data de nastere").Must(BeAValidDate);
            RuleFor(c => c.PhoneNumber).NotNull().WithMessage("Trebuie sa specificati un numar de telefon").Length(10, 10)
                .WithMessage("Numarul de telefon invalid");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
