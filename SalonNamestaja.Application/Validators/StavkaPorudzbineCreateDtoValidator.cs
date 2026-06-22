using FluentValidation;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Validators
{
    public class StavkaPorudzbineCreateDtoValidator : AbstractValidator<StavkaPorudzbineCreateDto>
    {
        public StavkaPorudzbineCreateDtoValidator()
        {
            RuleFor(x => x.ProizvodID)
                .GreaterThan(0).WithMessage("Proizvod je obavezan.");

            RuleFor(x => x.Kolicina)
                .GreaterThan(0).WithMessage("Količina mora biti veća od 0.");
        }
    }
}
