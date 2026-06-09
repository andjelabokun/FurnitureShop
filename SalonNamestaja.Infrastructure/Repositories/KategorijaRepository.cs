using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestaja.Infrastructure.Data;

namespace SalonNamestaja.Infrastructure.Repositories
{
    public class KategorijaRepository : Repository<Kategorija>, IKategorijaRepository
    {
        public KategorijaRepository(AppDbContext context) : base(context) { }

        public Kategorija? GetByIdSaPodkategorijama(int id) =>
            DbSet.Include(k => k.Podkategorije)
                 .FirstOrDefault(k => k.KategorijaID == id);
    }
}
