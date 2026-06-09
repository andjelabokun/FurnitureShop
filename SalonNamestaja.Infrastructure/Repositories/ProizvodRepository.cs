using System;
using System.Collections.Generic;
using System.Text;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestaja.Infrastructure.Data;

namespace SalonNamestaja.Infrastructure.Repositories
{
    public class ProizvodRepository : Repository<Proizvod>, IProizvodRepository
    {
        public ProizvodRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Proizvod> GetSviBojom(int bojaId) =>
            DbSet.Where(p => p.BojaID == bojaId).ToList();
    }
}
