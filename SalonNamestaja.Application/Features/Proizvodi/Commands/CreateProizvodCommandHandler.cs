using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Proizvodi.Commands
{
    public class CreateProizvodCommandHandler : IRequestHandler<CreateProizvodCommand, Proizvod>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProizvodCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Proizvod> Handle(CreateProizvodCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            Proizvod proizvod = dto.TipProizvoda switch
            {
                "Garnitura" => new Garnitura
                {
                    Punjenje = dto.Punjenje ?? string.Empty,
                    Orijentacija = dto.Orijentacija ?? string.Empty,
                    BrojMesta = dto.BrojMesta ?? 0,
                    Rasklopiva = dto.Rasklopiva ?? false
                },

                "Krevet" => new Krevet
                {
                    DimenzijaDuseka = dto.DimenzijaDuseka ?? string.Empty,
                    ImaSanduk = dto.ImaSanduk ?? false,
                    TipKreveta = dto.TipKreveta ?? string.Empty
                },

                "Orman" => new Orman
                {
                    BrojVrata = dto.BrojVrata ?? 0,
                    ImaOgledalo = dto.ImaOgledalo ?? false,
                    TipVrata = dto.TipVrata ?? string.Empty
                },

                "Sto" => new Sto
                {
                    Oblik = dto.Oblik ?? string.Empty,
                    BrojMesta = dto.BrojMesta ?? 0,
                    Rasklopiv = dto.Rasklopiv ?? false
                },

                _ => new Proizvod()
            };

            proizvod.Naziv = dto.Naziv;
            proizvod.Opis = dto.Opis;
            proizvod.Cena = dto.Cena;
            proizvod.StanjeNaLageru = dto.StanjeNaLageru;

            proizvod.PodkategorijaID = dto.PodkategorijaId;
            proizvod.MaterijalID = dto.MaterijalId;
            proizvod.BojaID = dto.BojaId;
            proizvod.DimenzijeID = dto.DimenzijeId;
            proizvod.ProizvodjacID = dto.ProizvodjacId;

            proizvod.SlikaUrl = dto.SlikaUrl;
            proizvod.TipProizvoda = string.IsNullOrWhiteSpace(dto.TipProizvoda)
                ? "Proizvod"
                : dto.TipProizvoda;

            _unitOfWork.Proizvodi.Add(proizvod);
            _unitOfWork.SaveChanges();

            return Task.FromResult(proizvod);
        }
    }
}