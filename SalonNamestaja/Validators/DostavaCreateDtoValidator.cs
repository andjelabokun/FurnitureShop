using FluentValidation;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Validators
{
    public class DostavaCreateDtoValidator : AbstractValidator<DostavaCreateDto>
    {
        public DostavaCreateDtoValidator()
        {
            RuleFor(x => x.DatumDostave)
                .NotEmpty().WithMessage("Datum dostave je obavezan.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status dostave je obavezan.");

            RuleFor(x => x.CenaDostave)
                .GreaterThanOrEqualTo(0).WithMessage("Cena dostave ne može biti negativna.");

            RuleFor(x => x.PorudzbinaID)
                .GreaterThan(0).WithMessage("Porudžbina je obavezna.");
        }
    }
}
