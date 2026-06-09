using FluentValidation;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Validators
{
    public class ProdavacCreateDtoValidator : AbstractValidator<ProdavacCreateDto>
    {
        public ProdavacCreateDtoValidator()
        {
            RuleFor(x => x.Ime)
                .NotEmpty().WithMessage("Ime je obavezno.")
                .MaximumLength(50).WithMessage("Ime može imati najviše 50 karaktera.");

            RuleFor(x => x.Prezime)
                .NotEmpty().WithMessage("Prezime je obavezno.")
                .MaximumLength(50).WithMessage("Prezime može imati najviše 50 karaktera.");

            RuleFor(x => x.KorisnickoIme)
                .NotEmpty().WithMessage("Korisničko ime je obavezno.")
                .MaximumLength(50).WithMessage("Korisničko ime može imati najviše 50 karaktera.");

            RuleFor(x => x.Lozinka)
                .NotEmpty().WithMessage("Lozinka je obavezna.")
                .MinimumLength(6).WithMessage("Lozinka mora imati najmanje 6 karaktera.");
        }
    }
}
