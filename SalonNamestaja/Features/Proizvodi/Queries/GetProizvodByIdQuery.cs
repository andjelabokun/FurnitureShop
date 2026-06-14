using MediatR;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetProizvodByIdQuery : IRequest<ProizvodDto?>
    {
        public int Id { get; set; }

        public GetProizvodByIdQuery(int id)
        {
            Id = id;
        }
    }
}
