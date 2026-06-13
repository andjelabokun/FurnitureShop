using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Porudzbine.Commands
{
    public class CreatePorudzbinaCommand : IRequest<int>
    {
        public PorudzbinaCreateDto Dto { get; set; }

        public CreatePorudzbinaCommand(PorudzbinaCreateDto dto)
        {
            Dto = dto;
        }
    }
}
