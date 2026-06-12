using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Prodavci.Commands
{
    public class DeleteProdavacCommandHandler : IRequestHandler<DeleteProdavacCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProdavacCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteProdavacCommand request, CancellationToken cancellationToken)
        {
            var prodavac = _unitOfWork.Prodavci.GetById(request.Id);

            if (prodavac == null)
                return Task.FromResult(false);

            _unitOfWork.Prodavci.Remove(prodavac);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
