using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Porudzbine.Commands
{
    public class UpdatePorudzbinaCommandHandler : IRequestHandler<UpdatePorudzbinaCommand, Porudzbina?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePorudzbinaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Porudzbina?> Handle(UpdatePorudzbinaCommand request, CancellationToken cancellationToken)
        {
            var porudzbina = _unitOfWork.Porudzbine.GetById(request.Id);

            if (porudzbina == null)
                return Task.FromResult<Porudzbina?>(null);

            porudzbina.Status = request.Dto.Status;
            porudzbina.UkupanIznos = request.Dto.UkupanIznos;
            porudzbina.KupacID = request.Dto.KupacID;
            porudzbina.ProdavacID = request.Dto.ProdavacID;

            _unitOfWork.Porudzbine.Update(porudzbina);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Porudzbina?>(porudzbina);
        }
    }
}
