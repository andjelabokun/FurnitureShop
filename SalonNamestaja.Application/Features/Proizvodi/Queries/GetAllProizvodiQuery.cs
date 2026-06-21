using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
{
    public class GetAllProizvodiQuery : IRequest<IEnumerable<ProizvodDto>>
    {
    }
}
