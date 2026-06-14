using MediatR;
using SalonNamestaja.Domain;
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
            var proizvodi = _unitOfWork.Proizvodi
                .GetAllSaDimenzijama()
                .Select(p => MapirajProizvod(p))
                .ToList();

            return Task.FromResult(proizvodi);
        }

        private ProizvodDto MapirajProizvod(Proizvod p)
        {
            var dto = new ProizvodDto
            {
                ProizvodID = p.ProizvodID,
                Naziv = p.Naziv,
                Opis = p.Opis,
                Cena = p.Cena,
                StanjeNaLageru = p.StanjeNaLageru,

                PodkategorijaId = p.PodkategorijaID,
                MaterijalId = p.MaterijalID,
                BojaId = p.BojaID,
                DimenzijeId = p.DimenzijeID,
                ProizvodjacId = p.ProizvodjacID,

                SlikaUrl = p.SlikaUrl,
                TipProizvoda = p.TipProizvoda,

                Sirina = p.Dimenzije != null ? p.Dimenzije.Sirina : null,
                Visina = p.Dimenzije != null ? p.Dimenzije.Visina : null,
                Dubina = p.Dimenzije != null ? p.Dimenzije.Dubina : null
            };

            if (p is Garnitura garnitura)
            {
                dto.Punjenje = garnitura.Punjenje;
                dto.Orijentacija = garnitura.Orijentacija;
                dto.BrojMesta = garnitura.BrojMesta;
                dto.Rasklopiva = garnitura.Rasklopiva;
            }

            if (p is Krevet krevet)
            {
                dto.DimenzijaDuseka = krevet.DimenzijaDuseka;
                dto.ImaSanduk = krevet.ImaSanduk;
                dto.TipKreveta = krevet.TipKreveta;
            }

            if (p is Orman orman)
            {
                dto.BrojVrata = orman.BrojVrata;
                dto.ImaOgledalo = orman.ImaOgledalo;
                dto.TipVrata = orman.TipVrata;
            }

            if (p is Sto sto)
            {
                dto.Oblik = sto.Oblik;
                dto.BrojMesta = sto.BrojMesta;
                dto.Rasklopiv = sto.Rasklopiv;
            }

            return dto;
        }
    }
}