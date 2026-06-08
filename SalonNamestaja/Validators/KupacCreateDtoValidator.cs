using FluentValidation;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Validators
{
    public class KupacCreateDtoValidator : AbstractValidator<KupacCreateDto>
    {
        public KupacCreateDtoValidator()
        {
            RuleFor(x => x.Ime)
                .NotEmpty().WithMessage("Ime je obavezno.")
                .MaximumLength(50);

            RuleFor(x => x.Prezime)
                .NotEmpty().WithMessage("Prezime je obavezno.")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email je obavezan.")
                .EmailAddress().WithMessage("Email nije u ispravnom formatu.");

            RuleFor(x => x.Telefon)
                .NotEmpty().WithMessage("Telefon je obavezan.")
                .MaximumLength(20);

            RuleFor(x => x.PIB)
                .GreaterThan(0)
                .When(x => x.PIB.HasValue)
                .WithMessage("PIB mora biti veći od 0.");
        }
    }
}
