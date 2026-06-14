using MediatR;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetAllProizvodiQuery : IRequest<IEnumerable<ProizvodDto>>
    {
    }
}
