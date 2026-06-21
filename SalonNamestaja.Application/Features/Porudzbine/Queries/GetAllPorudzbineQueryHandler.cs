using MediatR;
using Microsoft.AspNetCore.Identity;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestaja.Infrastructure.Identity;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Porudzbine.Queries
{
    public class GetAllPorudzbineQueryHandler
        : IRequestHandler<GetAllPorudzbineQuery, IEnumerable<PorudzbinaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllPorudzbineQueryHandler(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<PorudzbinaDto>> Handle(
            GetAllPorudzbineQuery request,
            CancellationToken cancellationToken)
        {
            var porudzbine = _unitOfWork.Porudzbine.GetAllSaStavkama();
            var rezultat = new List<PorudzbinaDto>();

            foreach (var p in porudzbine)
            {
                var kupac = await _userManager.FindByIdAsync(p.ApplicationUserId);

                rezultat.Add(new PorudzbinaDto
                {
                    PorudzbinaID = p.PorudzbinaID,
                    DatumVreme = p.DatumVreme,
                    Status = p.Status,
                    UkupanIznos = p.UkupanIznos,
                    ApplicationUserId = p.ApplicationUserId,

                    KupacIme = kupac != null ? kupac.Ime : null,
                    KupacPrezime = kupac != null ? kupac.Prezime : null,
                    KupacEmail = kupac != null ? kupac.Email : null,
                    KupacTelefon = kupac != null ? kupac.Telefon : null,
                    Adresa = kupac != null ? kupac.AdresaIsporuke : null,

                    Stavke = p.StavkePorudzbine != null
                        ? p.StavkePorudzbine.Select(s => new StavkaPorudzbineDto
                        {
                            ProizvodID = s.ProizvodID,
                            ProizvodNaziv = s.Proizvod != null ? s.Proizvod.Naziv : null,
                            Kolicina = s.Kolicina,
                            CenaPoKomadu = s.CenaPoKomadu,
                            Iznos = s.Iznos
                        }).ToList()
                        : new List<StavkaPorudzbineDto>()
                });
            }

            return rezultat;
        }
    }
}