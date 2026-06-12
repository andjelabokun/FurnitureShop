using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Materijali.Commands
{
    public class UpdateMaterijalCommand : IRequest<Materijal?>
    {
        public int Id { get; set; }
        public MaterijalDto Dto { get; set; }

        public UpdateMaterijalCommand(int id, MaterijalDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
