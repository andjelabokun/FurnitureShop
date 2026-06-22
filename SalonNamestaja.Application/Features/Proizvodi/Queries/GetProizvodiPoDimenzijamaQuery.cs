using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
{
    public class GetProizvodiPoDimenzijamaQuery : IRequest<IEnumerable<ProizvodDto>>
    {
        public double? MaxSirina { get; set; }
        public double? MaxVisina { get; set; }
        public double? MaxDubina { get; set; }

        public GetProizvodiPoDimenzijamaQuery(
            double? maxSirina,
            double? maxVisina,
            double? maxDubina)
        {
            MaxSirina = maxSirina;
            MaxVisina = maxVisina;
            MaxDubina = maxDubina;
        }
    }
}
