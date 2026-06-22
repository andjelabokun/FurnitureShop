using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Proizvod> GetAllSaDimenzijama()
        {
            return Context.Proizvodi
                .Include(p => p.Dimenzije)
                .ToList();
        }

        public IEnumerable<Proizvod> GetSviSaMaterijalom(int materijalId)=>
            DbSet.Where(p => p.MaterijalID == materijalId).ToList();

      

        public IEnumerable<Proizvod> GetSviSaPodKategorijom(int podKategorijaId) =>
            DbSet.Where(p => p.PodkategorijaID == podKategorijaId).ToList();

        public IEnumerable<Proizvod> GetSviPoKategoriji(int kategorijaId)
        {
            var podkategorijeIds = Context.PodKategorije
                      .Where(pk => pk.KategorijaID == kategorijaId)
                      .Select(pk => pk.PodkategorijaID)
                      .ToList();

            return Context.Proizvodi
                .Where(p => podkategorijeIds.Contains(p.PodkategorijaID))
                .ToList();
        }

        public IEnumerable<Proizvod> GetSviSaMaxCenom(double maxCena) =>
                DbSet.Where(p => p.Cena <= maxCena).ToList();


        public IEnumerable<Proizvod> GetSviPoDimenzijama(double? maxSirina, double? maxVisina, double? maxDubina)
        {
            var query = Context.Proizvodi
                 .Include(p => p.Dimenzije)
                 .AsQueryable();

            if (maxSirina.HasValue)
            {
                query = query.Where(p => p.Dimenzije != null &&
                                         p.Dimenzije.Sirina <= maxSirina.Value);
            }

            if (maxVisina.HasValue)
            {
                query = query.Where(p => p.Dimenzije != null &&
                                         p.Dimenzije.Visina <= maxVisina.Value);
            }

            if (maxDubina.HasValue)
            {
                query = query.Where(p => p.Dimenzije != null &&
                                         p.Dimenzije.Dubina <= maxDubina.Value);
            }

            return query.ToList();
        }
    }
}
