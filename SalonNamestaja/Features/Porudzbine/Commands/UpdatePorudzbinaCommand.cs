using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Porudzbine.Commands
{
    public class UpdatePorudzbinaCommand : IRequest<Porudzbina?>
    {
        public int Id { get; set; }
        public PorudzbinaCreateDto Dto { get; set; }

        public UpdatePorudzbinaCommand(int id, PorudzbinaCreateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
