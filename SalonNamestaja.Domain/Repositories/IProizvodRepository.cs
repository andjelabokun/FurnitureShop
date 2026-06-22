using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain.Repositories
{
    public interface IProizvodRepository : IRepository<Proizvod>
    {
        IEnumerable<Proizvod> GetSviBojom(int bojaId);
        IEnumerable<Proizvod> GetAllSaDimenzijama();

        IEnumerable<Proizvod> GetSviSaMaterijalom(int materijalId);

        IEnumerable<Proizvod> GetSviSaPodKategorijom(int podKategorijaId);
        IEnumerable<Proizvod> GetSviPoKategoriji(int kategorijaId);

        IEnumerable<Proizvod> GetSviSaMaxCenom(double maxCena);
        IEnumerable<Proizvod> GetSviPoDimenzijama(double? maxSirina, double? maxVisina, double? maxDubina);




    }
}
