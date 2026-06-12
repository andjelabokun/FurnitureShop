using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetProizvodiPoBojiQuery : IRequest<IEnumerable<Proizvod>>
    {
        public int BojaId { get; set; }

        public GetProizvodiPoBojiQuery(int bojaId)
        {
            BojaId = bojaId;
        }
    }
}
