using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Proizvodjaci.Commands
{
    public class DeleteProizvodjacCommandHandler : IRequestHandler<DeleteProizvodjacCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProizvodjacCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteProizvodjacCommand request, CancellationToken cancellationToken)
        {
            var proizvodjac = _unitOfWork.Proizvodjaci.GetById(request.Id);

            if (proizvodjac == null)
                return Task.FromResult(false);

            var postojiProizvod = _unitOfWork.Proizvodi
                .GetAll()
                .Any(p => p.ProizvodjacID == request.Id);

            if (postojiProizvod)
                throw new InvalidOperationException("Proizvođač ne može biti obrisan jer postoji proizvod koji ga koristi.");

            _unitOfWork.Proizvodjaci.Remove(proizvodjac);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}