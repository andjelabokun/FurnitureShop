using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetProizvodiPoBojiQueryHandler
        : IRequestHandler<GetProizvodiPoBojiQuery, IEnumerable<ProizvodDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProizvodiPoBojiQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<ProizvodDto>> Handle(
            GetProizvodiPoBojiQuery request,
            CancellationToken cancellationToken)
        {
            var proizvodi = _unitOfWork.Proizvodi.GetSviBojom(request.BojaId);

            var rezultat = proizvodi
                .Select(p => MapirajProizvod(p))
                .ToList();

            return Task.FromResult<IEnumerable<ProizvodDto>>(rezultat);
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
                TipProizvoda = p.TipProizvoda
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