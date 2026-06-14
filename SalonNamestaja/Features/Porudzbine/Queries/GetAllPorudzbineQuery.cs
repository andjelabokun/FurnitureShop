using MediatR;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Porudzbine.Queries
{
    public class GetAllPorudzbineQuery : IRequest<IEnumerable<PorudzbinaDto>>
    {
    }
}
