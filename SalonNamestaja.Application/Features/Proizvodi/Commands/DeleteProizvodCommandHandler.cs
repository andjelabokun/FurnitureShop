using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Proizvodi.Commands
{
    public class DeleteProizvodCommandHandler : IRequestHandler<DeleteProizvodCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProizvodCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteProizvodCommand request, CancellationToken cancellationToken)
        {
            var proizvod = _unitOfWork.Proizvodi.GetById(request.Id);

            if (proizvod == null)
                return Task.FromResult(false);

            var postojiUPorudzbini = _unitOfWork.StavkePorudzbine
                .GetAll()
                .Any(s => s.ProizvodID == request.Id);

            if (postojiUPorudzbini)
                throw new InvalidOperationException("Proizvod ne može biti obrisan jer se nalazi u nekoj porudžbini.");

            _unitOfWork.Proizvodi.Remove(proizvod);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}