using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Boje.Commands
{
    public class DeleteBojaCommandHandler : IRequestHandler<DeleteBojaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBojaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteBojaCommand request, CancellationToken cancellationToken)
        {
            var boja = _unitOfWork.Boje.GetById(request.Id);

            if (boja == null)
                return Task.FromResult(false);

            var postojiProizvod = _unitOfWork.Proizvodi
                .GetAll()
                .Any(p => p.BojaID == request.Id);

            if (postojiProizvod)
                throw new InvalidOperationException("Boja ne može biti obrisana jer postoji proizvod koji je koristi.");

            _unitOfWork.Boje.Remove(boja);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}