using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Porudzbine.Queries
{
    public class GetPorudzbineFilterQuery : IRequest<IEnumerable<PorudzbinaDto>>
    {
        public string? Pretraga { get; set; }
        public string? Status { get; set; }
        public DateTime? DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }

        public GetPorudzbineFilterQuery(
            string? pretraga,
            string? status,
            DateTime? datumOd,
            DateTime? datumDo)
        {
            Pretraga = pretraga;
            Status = status;
            DatumOd = datumOd;
            DatumDo = datumDo;
        }
    }
}
