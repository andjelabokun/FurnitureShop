using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Porudzbine.Commands
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
