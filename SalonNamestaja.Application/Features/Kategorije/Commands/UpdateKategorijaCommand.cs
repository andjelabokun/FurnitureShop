using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Kategorije.Commands
{
    public class UpdateKategorijaCommand : IRequest<Kategorija?>
    {
        public int Id { get; set; }
        public KategorijaUpdateDto Dto { get; set; }

        public UpdateKategorijaCommand(int id, KategorijaUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
