using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Proizvodi.Commands
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

            _unitOfWork.Proizvodi.Remove(proizvod);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
