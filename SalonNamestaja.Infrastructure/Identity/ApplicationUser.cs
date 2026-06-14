using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SalonNamestaja.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string? Telefon { get; set; }
        public string? AdresaIsporuke { get; set; }
        public int? PIB { get; set; }
        public string? TipKupca { get; set; }
    }
}
