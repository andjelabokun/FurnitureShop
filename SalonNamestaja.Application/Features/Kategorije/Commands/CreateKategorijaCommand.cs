using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Kategorije.Commands
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
