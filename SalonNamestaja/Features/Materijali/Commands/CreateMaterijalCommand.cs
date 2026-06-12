using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Materijali.Commands
{
    public class CreateMaterijalCommand : IRequest<Materijal>
    {
        public MaterijalDto Dto { get; set; }

        public CreateMaterijalCommand(MaterijalDto dto)
        {
            Dto = dto;
        }
    }
}
