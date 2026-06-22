using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestaja.Application.Features.Proizvodjaci.Queries
{
    public class GetAllProizvodjaciQuery : IRequest<IEnumerable<Proizvodjac>>
    {
    }
}
