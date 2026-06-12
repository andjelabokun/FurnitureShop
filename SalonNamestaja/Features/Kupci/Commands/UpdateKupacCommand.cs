using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Kupci.Commands
{
    public class UpdateKupacCommand : IRequest<Kupac?>
    {
        public int Id { get; set; }
        public KupacUpdateDto Dto { get; set; }

        public UpdateKupacCommand(int id, KupacUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
