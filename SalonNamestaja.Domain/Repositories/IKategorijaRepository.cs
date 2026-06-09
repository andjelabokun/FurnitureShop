using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Domain.Repositories
{
    public interface IKategorijaRepository : IRepository<Kategorija>
    {
        Kategorija? GetByIdSaPodkategorijama(int id);
    }
}
