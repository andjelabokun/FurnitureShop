using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Boje.Commands
{
    public class UpdateBojaCommand : IRequest<Boja?>
    {
        public int Id { get; set; }
        public BojaDto Dto { get; set; }

        public UpdateBojaCommand(int id, BojaDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
