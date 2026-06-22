using MediatR;
using Microsoft.AspNetCore.Identity;
using SalonNamestaja.Application.DTOs;
using SalonNamestaja.Domain.Repositories;
using SalonNamestaja.Infrastructure.Identity;

namespace SalonNamestaja.Application.Features.Porudzbine.Queries
{
    public class GetPorudzbineFilterQueryHandler
        : IRequestHandler<GetPorudzbineFilterQuery, IEnumerable<PorudzbinaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetPorudzbineFilterQueryHandler(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<PorudzbinaDto>> Handle(
            GetPorudzbineFilterQuery request,
            CancellationToken cancellationToken)
        {
            var porudzbine = _unitOfWork.Porudzbine.GetFiltriranePorudzbine(
                request.Pretraga,
                request.Status,
                request.DatumOd,
                request.DatumDo);

            var rezultat = new List<PorudzbinaDto>();

            foreach (var p in porudzbine)
            {
                var kupac = await _userManager.FindByIdAsync(p.ApplicationUserId);

                var dto = new PorudzbinaDto
                {
                    PorudzbinaID = p.PorudzbinaID,
                    DatumVreme = p.DatumVreme,
                    Status = p.Status,
                    UkupanIznos = p.UkupanIznos,
                    ApplicationUserId = p.ApplicationUserId,

                    KupacIme = kupac?.Ime ?? "",
                    KupacPrezime = kupac?.Prezime ?? "",
                    KupacEmail = kupac?.Email ?? "",
                    KupacTelefon = kupac?.Telefon ?? "",
                    Adresa = kupac?.AdresaIsporuke ?? "",

                    Stavke = p.StavkePorudzbine.Select(s => new StavkaPorudzbineDto
                    {
                        
                        ProizvodID = s.ProizvodID,
                        ProizvodNaziv = s.Proizvod != null ? s.Proizvod.Naziv : "",
                        Kolicina = s.Kolicina,
                        CenaPoKomadu = s.CenaPoKomadu,
                        Iznos = s.Iznos
                    }).ToList()
                };

                rezultat.Add(dto);
            }

            return rezultat;
        }
    }
}
