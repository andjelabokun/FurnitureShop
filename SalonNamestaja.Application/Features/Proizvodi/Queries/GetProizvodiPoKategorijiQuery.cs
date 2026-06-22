using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
{
    public class GetProizvodiPoKategorijiQuery : IRequest<IEnumerable<ProizvodDto>>
    {
        public int KategorijaId { get; set; }

        public GetProizvodiPoKategorijiQuery(int kategorijaId)
        {
            KategorijaId = kategorijaId;
        }
    }
}
