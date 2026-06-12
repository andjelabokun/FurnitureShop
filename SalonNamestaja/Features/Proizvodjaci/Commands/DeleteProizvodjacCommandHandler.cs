using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Proizvodjaci.Commands
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

            _unitOfWork.Proizvodjaci.Remove(proizvodjac);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
