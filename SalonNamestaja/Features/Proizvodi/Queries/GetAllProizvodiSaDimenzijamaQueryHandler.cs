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
                    BojaID = p.BojaID,
                    Sirina = p.Dimenzije?.Sirina,
                    Visina = p.Dimenzije?.Visina,
                    Dubina = p.Dimenzije?.Dubina
                }).ToList();

            return Task.FromResult(proizvodi);
        }
    }
}
