using FluentValidation;

namespace WebApp.Models.Validations
{
    public class BloodDonorValidator : AbstractValidator<BloodDonorModel>
    {
        public BloodDonorValidator()
        {
            RuleFor(c => c.Type).NotNull().WithMessage("Trebuie sa specificati un tip").Matches(@"^(01|A2|B3|AB4)$").WithMessage("Nu este un tip valid");
            RuleFor(c => c.PatientId).NotNull();
        }
    }
}
