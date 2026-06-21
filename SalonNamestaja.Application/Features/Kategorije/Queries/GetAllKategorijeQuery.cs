using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Kategorije.Queries
{
    public class GetAllKategorijeQuery : IRequest<IEnumerable<KategorijaDto>>
    {
    }
}
