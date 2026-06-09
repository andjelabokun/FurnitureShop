using FluentValidation;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Validators
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
