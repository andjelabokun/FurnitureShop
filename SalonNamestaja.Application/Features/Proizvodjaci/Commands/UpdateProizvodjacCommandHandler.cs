using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Proizvodjaci.Commands
{
    public class UpdateProizvodjacCommandHandler : IRequestHandler<UpdateProizvodjacCommand, Proizvodjac?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProizvodjacCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Proizvodjac?> Handle(UpdateProizvodjacCommand request, CancellationToken cancellationToken)
        {
            var proizvodjac = _unitOfWork.Proizvodjaci.GetById(request.Id);

            if (proizvodjac == null)
                return Task.FromResult<Proizvodjac?>(null);

            proizvodjac.Naziv = request.Dto.Naziv;
            proizvodjac.Drzava = request.Dto.Drzava;

            _unitOfWork.Proizvodjaci.Update(proizvodjac);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Proizvodjac?>(proizvodjac);
        }
    }
}
