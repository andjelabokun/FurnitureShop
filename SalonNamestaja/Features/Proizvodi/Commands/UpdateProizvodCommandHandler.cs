using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Proizvodi.Commands
{
    public class UpdateProizvodCommandHandler : IRequestHandler<UpdateProizvodCommand, Proizvod?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProizvodCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Proizvod?> Handle(UpdateProizvodCommand request, CancellationToken cancellationToken)
        {
            var proizvod = _unitOfWork.Proizvodi.GetById(request.Id);

            if (proizvod == null)
                return Task.FromResult<Proizvod?>(null);

            var dto = request.Dto;

            // Zajednička polja za svaki proizvod
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

            // Posebna polja za Garnituru
            if (proizvod is Garnitura garnitura)
            {
                garnitura.Punjenje = dto.Punjenje ?? string.Empty;
                garnitura.Orijentacija = dto.Orijentacija ?? string.Empty;
                garnitura.BrojMesta = dto.BrojMesta ?? 0;
                garnitura.Rasklopiva = dto.Rasklopiva ?? false;
            }

            // Posebna polja za Krevet
            if (proizvod is Krevet krevet)
            {
                krevet.DimenzijaDuseka = dto.DimenzijaDuseka ?? string.Empty;
                krevet.ImaSanduk = dto.ImaSanduk ?? false;
                krevet.TipKreveta = dto.TipKreveta ?? string.Empty;
            }

            // Posebna polja za Orman
            if (proizvod is Orman orman)
            {
                orman.BrojVrata = dto.BrojVrata ?? 0;
                orman.ImaOgledalo = dto.ImaOgledalo ?? false;
                orman.TipVrata = dto.TipVrata ?? string.Empty;
            }

            // Posebna polja za Sto
            if (proizvod is Sto sto)
            {
                sto.Oblik = dto.Oblik ?? string.Empty;
                sto.BrojMesta = dto.BrojMesta ?? 0;
                sto.Rasklopiv = dto.Rasklopiv ?? false;
            }

            _unitOfWork.Proizvodi.Update(proizvod);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Proizvod?>(proizvod);
        }
    }
}