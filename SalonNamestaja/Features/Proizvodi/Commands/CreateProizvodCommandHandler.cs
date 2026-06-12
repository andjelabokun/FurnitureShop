using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Proizvodi.Commands
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

            var proizvod = new Proizvod
            {
                Naziv = dto.Naziv,
                Opis = dto.Opis,
                Cena = dto.Cena,
                StanjeNaLageru = dto.StanjeNaLageru,
                PodkategorijaID = dto.PodkategorijaId,
                MaterijalID = dto.MaterijalId,
                BojaID = dto.BojaId,
                DimenzijeID = dto.DimenzijeId,
                ProizvodjacID = dto.ProizvodjacId,
                SlikaUrl = dto.SlikaUrl
            };

            _unitOfWork.Proizvodi.Add(proizvod);
            _unitOfWork.SaveChanges();

            return Task.FromResult(proizvod);
        }
    }
}
