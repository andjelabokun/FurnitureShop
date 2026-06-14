using MediatR;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetProizvodiPoBojiQuery : IRequest<IEnumerable<ProizvodDto>>
    {
        public int BojaId { get; set; }

        public GetProizvodiPoBojiQuery(int bojaId)
        {
            BojaId = bojaId;
        }
    }
}