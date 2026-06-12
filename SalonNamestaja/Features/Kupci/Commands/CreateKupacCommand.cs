using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Kupci.Commands
{
    public class CreateKupacCommand : IRequest<Kupac>
    {
        public KupacCreateDto Dto { get; set; }

        public CreateKupacCommand(KupacCreateDto dto)
        {
            Dto = dto;
        }
    }
}
