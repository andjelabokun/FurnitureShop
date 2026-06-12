using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodi.Commands
{
    public class UpdateProizvodCommand : IRequest<Proizvod?>
    {
        public int Id { get; set; }
        public ProizvodUpdateDto Dto { get; set; }

        public UpdateProizvodCommand(int id, ProizvodUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
