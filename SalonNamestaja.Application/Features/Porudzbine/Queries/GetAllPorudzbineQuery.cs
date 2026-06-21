using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Porudzbine.Queries
{
    public class GetAllPorudzbineQuery : IRequest<IEnumerable<PorudzbinaDto>>
    {
    }
}
