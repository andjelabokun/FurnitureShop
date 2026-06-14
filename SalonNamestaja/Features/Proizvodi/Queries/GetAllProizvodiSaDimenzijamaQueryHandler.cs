using MediatR;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetAllProizvodiSaDimenzijamaQueryHandler
        : IRequestHandler<GetAllProizvodiSaDimenzijamaQuery, List<ProizvodDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProizvodiSaDimenzijamaQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<ProizvodDto>> Handle(
            GetAllProizvodiSaDimenzijamaQuery request,
            CancellationToken cancellationToken)
        {
            var proizvodi = _unitOfWork.Proizvodi.GetAllSaDimenzijama()
                .Select(p => new ProizvodDto
                {
                    ProizvodID = p.ProizvodID,
                    Naziv = p.Naziv,
                    Opis = p.Opis,
                    Cena = p.Cena,
                    StanjeNaLageru = p.StanjeNaLageru,

                    PodkategorijaID = p.PodkategorijaID,
                    BojaID = p.BojaID,

                    Sirina = p.Dimenzije != null ? p.Dimenzije.Sirina : null,
                    Visina = p.Dimenzije != null ? p.Dimenzije.Visina : null,
                    Dubina = p.Dimenzije != null ? p.Dimenzije.Dubina : null,

                    SlikaUrl = p.SlikaUrl
                })
                .ToList();

            return Task.FromResult(proizvodi);
        }
    }
}