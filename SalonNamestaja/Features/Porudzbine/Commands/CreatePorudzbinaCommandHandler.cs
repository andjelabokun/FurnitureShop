using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Porudzbine.Commands
{
    public class CreatePorudzbinaCommandHandler : IRequestHandler<CreatePorudzbinaCommand, Porudzbina>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePorudzbinaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Porudzbina> Handle(CreatePorudzbinaCommand request, CancellationToken cancellationToken)
        {
            var porudzbina = new Porudzbina
            {
                DatumVreme = DateTime.Now,
                Status = request.Dto.Status,
                UkupanIznos = request.Dto.UkupanIznos,
                KupacID = request.Dto.KupacID,
                ProdavacID = request.Dto.ProdavacID
            };

            _unitOfWork.Porudzbine.Add(porudzbina);
            _unitOfWork.SaveChanges();

            return Task.FromResult(porudzbina);
        }
    }
}
