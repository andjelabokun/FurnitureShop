using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Materijali.Commands
{
    public class DeleteMaterijalCommandHandler : IRequestHandler<DeleteMaterijalCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMaterijalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteMaterijalCommand request, CancellationToken cancellationToken)
        {
            var materijal = _unitOfWork.Materijali.GetById(request.Id);

            if (materijal == null)
                return Task.FromResult(false);

            var postojiProizvod = _unitOfWork.Proizvodi
                .GetAll()
                .Any(p => p.MaterijalID == request.Id);

            if (postojiProizvod)
                throw new InvalidOperationException("Materijal ne može biti obrisan jer postoji proizvod koji ga koristi.");

            _unitOfWork.Materijali.Remove(materijal);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}