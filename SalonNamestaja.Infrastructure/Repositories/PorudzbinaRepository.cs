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

        public IEnumerable<Porudzbina> GetFiltriranePorudzbine(string? pretraga, string? status, DateTime? datumOd, DateTime? datumDo)
        {
            var query = Context.Porudzbine
                        .Include(p => p.StavkePorudzbine)
                        .ThenInclude(s => s.Proizvod)
                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status) && status != "Svi statusi")
            {
                query = query.Where(p => p.Status.ToLower() == status.ToLower());
            }

            if (datumOd.HasValue)
            {
                query = query.Where(p => p.DatumVreme.Date >= datumOd.Value.Date);
            }

            if (datumDo.HasValue)
            {
                query = query.Where(p => p.DatumVreme.Date <= datumDo.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(pretraga))
            {
                var tekst = pretraga.ToLower();

                var jesteBroj = int.TryParse(tekst, out int porudzbinaId);

                query = query.Where(p =>
                    (jesteBroj && p.PorudzbinaID == porudzbinaId) ||
                    Context.Users.Any(u =>
                        u.Id == p.ApplicationUserId &&
                        (
                            ((u.Ime ?? "") + " " + (u.Prezime ?? "")).ToLower().Contains(tekst) ||
                            (u.Email ?? "").ToLower().Contains(tekst) ||
                            (u.Telefon ?? "").ToLower().Contains(tekst) ||
                            (u.AdresaIsporuke ?? "").ToLower().Contains(tekst)
                        )
                    )
                );
            }

            return query
                .OrderByDescending(p => p.DatumVreme)
                .ToList();
        }
    }
}
