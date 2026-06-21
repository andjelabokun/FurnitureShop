using FluentValidation;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Validators
{
    public class KategorijaCreateDtoValidator : AbstractValidator<KategorijaCreateDto>
    {
        public KategorijaCreateDtoValidator()
        {
            RuleFor(x => x.Naziv)
                .NotEmpty().WithMessage("Naziv kategorije je obavezan.")
                .MaximumLength(100);
        }
    }
}
