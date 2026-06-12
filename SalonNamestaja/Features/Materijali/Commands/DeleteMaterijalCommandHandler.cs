using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Materijali.Commands
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

            _unitOfWork.Materijali.Remove(materijal);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
