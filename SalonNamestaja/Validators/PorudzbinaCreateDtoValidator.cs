using FluentValidation;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Validators
{
    public class PorudzbinaCreateDtoValidator : AbstractValidator<PorudzbinaCreateDto>
    {
        public PorudzbinaCreateDtoValidator()
        {
            RuleFor(x => x.DatumVreme)
                .NotEmpty().WithMessage("Datum i vreme su obavezni.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status je obavezan.");

            RuleFor(x => x.UkupanIznos)
                .GreaterThanOrEqualTo(0).WithMessage("Ukupan iznos ne može biti negativan.");

            RuleFor(x => x.KupacID)
                .GreaterThan(0).WithMessage("Kupac je obavezan.");

            RuleFor(x => x.ProdavacID)
                .GreaterThan(0).WithMessage("Prodavac je obavezan.");

            RuleFor(x => x.Stavke)
                .NotEmpty().WithMessage("Porudžbina mora imati bar jednu stavku.");
        }
    }
}
