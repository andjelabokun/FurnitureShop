using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
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
