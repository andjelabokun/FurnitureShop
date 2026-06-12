using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Prodavci.Commands
{
    public class UpdateProdavacCommand : IRequest<Prodavac?>
    {
        public int Id { get; set; }
        public ProdavacUpdateDto Dto { get; set; }

        public UpdateProdavacCommand(int id, ProdavacUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
