using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Kupci.Commands
{
    public class DeleteKupacCommandHandler : IRequestHandler<DeleteKupacCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteKupacCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteKupacCommand request, CancellationToken cancellationToken)
        {
            var kupac = _unitOfWork.Kupci.GetById(request.Id);

            if (kupac == null)
                return Task.FromResult(false);

            _unitOfWork.Kupci.Remove(kupac);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
