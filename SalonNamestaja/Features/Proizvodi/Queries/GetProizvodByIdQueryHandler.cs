using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetProizvodByIdQueryHandler : IRequestHandler<GetProizvodByIdQuery, ProizvodDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProizvodByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ProizvodDto?> Handle(GetProizvodByIdQuery request, CancellationToken cancellationToken)
        {
            var proizvod = _unitOfWork.Proizvodi.GetById(request.Id);

            if (proizvod == null)
                return Task.FromResult<ProizvodDto?>(null);

            var dto = MapirajProizvod(proizvod);

            return Task.FromResult<ProizvodDto?>(dto);
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