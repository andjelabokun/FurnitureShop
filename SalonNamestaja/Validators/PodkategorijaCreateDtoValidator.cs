using FluentValidation;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Validators
{
    public class PodkategorijaCreateDtoValidator : AbstractValidator<PodkategorijaCreateDto>
    {
        public PodkategorijaCreateDtoValidator()
        {
            RuleFor(x => x.Naziv)
                .NotEmpty().WithMessage("Naziv podkategorije je obavezan.")
                .MaximumLength(100);

            RuleFor(x => x.KategorijaID)
                .GreaterThan(0).WithMessage("Kategorija je obavezna.");
        }
    }
}
