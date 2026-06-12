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

            proizvod.Naziv = dto.Naziv;
            proizvod.Opis = dto.Opis;
            proizvod.Cena = dto.Cena;
            proizvod.StanjeNaLageru = dto.StanjeNaLageru;
            proizvod.PodkategorijaID = dto.PodkategorijaId;
            proizvod.MaterijalID = dto.MaterijalId;
            proizvod.BojaID = dto.BojaId;
            proizvod.DimenzijeID = dto.DimenzijeId;
            proizvod.SlikaUrl = dto.SlikaUrl;

            _unitOfWork.Proizvodi.Update(proizvod);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Proizvod?>(proizvod);
        }
    }
}
