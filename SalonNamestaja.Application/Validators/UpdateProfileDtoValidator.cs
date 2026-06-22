using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;
using SalonNamestaja.Application.DTOs.Auth;

namespace SalonNamestaja.Application.Validators
{
    public class UpdateProfileDtoValidator : AbstractValidator<UpdateProfileDto>
    {
        public UpdateProfileDtoValidator()
        {
            RuleFor(x => x.Telefon)
                .NotEmpty().WithMessage("Telefon je obavezan.");

            RuleFor(x => x.AdresaIsporuke)
                .NotEmpty().WithMessage("Adresa isporuke je obavezna.");

            RuleFor(x => x.PIB)
                .NotNull().When(x => x.TipKupca == "PravnoLice")
                .WithMessage("PIB je obavezan za pravna lica.");
        }
    }
}
