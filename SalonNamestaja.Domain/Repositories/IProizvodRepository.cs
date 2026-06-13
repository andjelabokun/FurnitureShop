using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain.Repositories
{
    public interface IProizvodRepository : IRepository<Proizvod>
    {
        IEnumerable<Proizvod> GetSviBojom(int bojaId);
        IEnumerable<Proizvod> GetAllSaDimenzijama();
    }
}
