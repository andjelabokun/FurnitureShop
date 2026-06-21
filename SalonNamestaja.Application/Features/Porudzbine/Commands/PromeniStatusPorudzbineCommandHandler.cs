using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Porudzbine.Commands
{
    public class PromeniStatusPorudzbineCommandHandler
        : IRequestHandler<PromeniStatusPorudzbineCommand, Porudzbina?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PromeniStatusPorudzbineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Porudzbina?> Handle(
            PromeniStatusPorudzbineCommand request,
            CancellationToken cancellationToken)
        {
            var porudzbina = _unitOfWork.Porudzbine.GetById(request.Id);

            if (porudzbina == null)
                return Task.FromResult<Porudzbina?>(null);

            porudzbina.Status = request.Status;

            _unitOfWork.Porudzbine.Update(porudzbina);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Porudzbina?>(porudzbina);
        }
    }
}
