using FluentValidation;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Validators
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
