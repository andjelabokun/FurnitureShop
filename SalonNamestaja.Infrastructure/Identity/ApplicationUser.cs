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
    }
}
