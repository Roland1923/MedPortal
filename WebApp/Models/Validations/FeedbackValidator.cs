using FluentValidation;

namespace WebApp.Models.Validations
{
    public class FeedbackValidator : AbstractValidator<FeedbackModel>
    {
        public FeedbackValidator()
        {
            RuleFor(c => c.Description).NotNull().WithMessage("Trebuie sa specificati o descriere").Length(4, 400)
                .WithMessage("Descrierea trebuie sa aiba intre 4 si 400 caractere");
            RuleFor(c => c.Rating).NotNull().InclusiveBetween(0,5);
            RuleFor(c => c.DoctorId).NotNull();
            RuleFor(c => c.PatientId).NotNull();
        }
    }
}
