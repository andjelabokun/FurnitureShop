using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Prodavci.Commands
{
    public class CreateProdavacCommand : IRequest<Prodavac>
    {
        public ProdavacCreateDto Dto { get; set; }

        public CreateProdavacCommand(ProdavacCreateDto dto)
        {
            Dto = dto;
        }
    }
}
