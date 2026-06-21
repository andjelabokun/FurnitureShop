using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Materijali.Commands
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
