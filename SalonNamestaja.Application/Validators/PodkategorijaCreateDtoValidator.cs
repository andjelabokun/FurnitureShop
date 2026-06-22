using FluentValidation;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Validators
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
