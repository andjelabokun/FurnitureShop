using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestaja.Infrastructure.Data;

namespace SalonNamestaja.Infrastructure.Repositories
{
    public class PorudzbinaRepository : Repository<Porudzbina>, IPorudzbinaRepository
    {
        private readonly AppDbContext _context;

        public PorudzbinaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Porudzbina> GetAllSaStavkama()
        {
            return _context.Porudzbine
                .Include(p => p.StavkePorudzbine)
                    .ThenInclude(s => s.Proizvod)
                .ToList();
        }
    }
}
