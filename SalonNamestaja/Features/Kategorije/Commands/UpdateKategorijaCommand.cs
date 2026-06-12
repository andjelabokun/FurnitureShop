using MediatR;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Kategorije.Commands
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
