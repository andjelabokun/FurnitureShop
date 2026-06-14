using MediatR;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Kategorije.Queries
{
    public class GetAllKategorijeQuery : IRequest<IEnumerable<KategorijaDto>>
    {
    }
}
