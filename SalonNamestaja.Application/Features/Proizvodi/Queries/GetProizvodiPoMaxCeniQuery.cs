using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
{
    public class GetProizvodiPoMaxCeniQuery : IRequest<IEnumerable<ProizvodDto>>
    {
        public double MaxCena { get; set; }

        public GetProizvodiPoMaxCeniQuery(double maxCena)
        {
            MaxCena = maxCena;
        }
    }
}
