using FluentValidation;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Validators
{
    public class ProizvodCreateDtoValidator : AbstractValidator<ProizvodCreateDto>
    {
        public ProizvodCreateDtoValidator()
        {
            RuleFor(x => x.Naziv)
                .NotEmpty().WithMessage("Naziv je obavezan.")
                .MaximumLength(100);

            RuleFor(x => x.Opis)
                .NotEmpty().WithMessage("Opis je obavezan.");

            RuleFor(x => x.Cena)
                .GreaterThan(0).WithMessage("Cena mora biti veća od 0.");

            RuleFor(x => x.StanjeNaLageru)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stanje na lageru ne može biti negativno.");
            RuleFor(x => x.BojaId)
                .GreaterThan(0).WithMessage("Boja je obavezna.");

            RuleFor(x => x.MaterijalId)
                .GreaterThan(0).WithMessage("Materijal je obavezan.");

            RuleFor(x => x.PodkategorijaId)
                .GreaterThan(0).WithMessage("Podkategorija je obavezna.");

            RuleFor(x => x.DimenzijeId)
                .GreaterThan(0).WithMessage("Dimenzije su obavezne.");

            RuleFor(x => x.ProizvodjacId)
                .GreaterThan(0).WithMessage("Proizvođač je obavezan.");
        }
    }
}
