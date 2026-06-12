using MediatR;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Porudzbine.Commands
{
    public class DeletePorudzbinaCommandHandler : IRequestHandler<DeletePorudzbinaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePorudzbinaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeletePorudzbinaCommand request, CancellationToken cancellationToken)
        {
            var porudzbina = _unitOfWork.Porudzbine.GetById(request.Id);

            if (porudzbina == null)
                return Task.FromResult(false);

            _unitOfWork.Porudzbine.Remove(porudzbina);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
