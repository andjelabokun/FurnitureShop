using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Dimenzije.Commands
{
    public class DeleteDimenzijeCommandHandler
        : IRequestHandler<DeleteDimenzijeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDimenzijeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(
            DeleteDimenzijeCommand request,
            CancellationToken cancellationToken)
        {
            var dimenzije = _unitOfWork.Dimenzije.GetById(request.Id);

            if (dimenzije == null)
                return Task.FromResult(false);

            var postojiProizvod = _unitOfWork.Proizvodi
                .GetAll()
                .Any(p => p.DimenzijeID == request.Id);

            if (postojiProizvod)
                throw new InvalidOperationException("Dimenzije ne mogu biti obrisane jer postoji proizvod koji ih koristi.");

            _unitOfWork.Dimenzije.Remove(dimenzije);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}