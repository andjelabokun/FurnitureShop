using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Kategorije.Commands
{
    public class CreateKategorijaCommand : IRequest<Kategorija>
    {
        public KategorijaCreateDto Dto { get; set; }

        public CreateKategorijaCommand(KategorijaCreateDto dto)
        {
            Dto = dto;
        }
    }
}
