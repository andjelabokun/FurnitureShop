using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Boje.Commands
{
    public class UpdateBojaCommandHandler : IRequestHandler<UpdateBojaCommand, Boja?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBojaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Boja?> Handle(UpdateBojaCommand request, CancellationToken cancellationToken)
        {
            var boja = _unitOfWork.Boje.GetById(request.Id);

            if (boja == null)
                return Task.FromResult<Boja?>(null);

            boja.Naziv = request.Dto.Naziv;

            _unitOfWork.Boje.Update(boja);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Boja?>(boja);
        }
    }
}
