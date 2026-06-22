using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
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