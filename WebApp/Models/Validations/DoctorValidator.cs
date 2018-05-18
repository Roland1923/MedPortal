using FluentValidation;

namespace WebApp.Models.Validations
{
    public class DoctorValidator : AbstractValidator<DoctorModel>
    {
        public DoctorValidator()
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
                .WithMessage("Numele orasului trebuie sa aiba intre 3 si 30 caractere");
            RuleFor(c => c.PhoneNumber).NotNull().WithMessage("Trebuie sa specificati un numar de telefon");
            //.Matches(@" ^\(? ([0 - 9]{ 3})\)?[-. ]? ([0 - 9]{3})[-. ]? ([0 - 9]{4})$")
            //.WithMessage("Numarul de telefon invalid");
            RuleFor(c => c.Description).Length(10, 50).WithMessage("Trebuie sa introduceti o descriere");
            RuleFor(c => c.Hospital).NotNull().WithMessage("Trebuie sa specificati spitalul");
            RuleFor(c => c.Address).NotNull().WithMessage("Trebuie sa specificati adresa");
            RuleFor(c => c.Speciality).NotNull().WithMessage("Trebuie sa specificati specialitatea");
        }
    }
}
