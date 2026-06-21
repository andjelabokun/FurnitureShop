using FluentValidation;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Validators
{
    public class PorudzbinaCreateDtoValidator : AbstractValidator<PorudzbinaCreateDto>
    {
        public PorudzbinaCreateDtoValidator()
        {
            

            RuleFor(x => x.UkupanIznos)
                .GreaterThanOrEqualTo(0).WithMessage("Ukupan iznos ne može biti negativan.");

            RuleFor(x => x.Stavke)
                .NotEmpty().WithMessage("Porudžbina mora imati bar jednu stavku.");
        }
    }
}
