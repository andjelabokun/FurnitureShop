using MediatR;
using SalonNamestaja.Application.DTOs;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
{
    public class GetProizvodiPoMaterijaluQueryHandler
     : IRequestHandler<GetProizvodiPoMaterijaluQuery, IEnumerable<ProizvodDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProizvodiPoMaterijaluQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<ProizvodDto>> Handle(
            GetProizvodiPoMaterijaluQuery request,
            CancellationToken cancellationToken)
        {
            var proizvodi = _unitOfWork.Proizvodi.GetSviSaMaterijalom(request.MaterijalId);

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
