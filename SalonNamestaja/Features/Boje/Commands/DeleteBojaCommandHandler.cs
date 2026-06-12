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

            _unitOfWork.Boje.Remove(boja);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
