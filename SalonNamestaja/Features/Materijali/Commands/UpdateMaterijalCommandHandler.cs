using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Materijali.Commands
{
    public class UpdateMaterijalCommandHandler : IRequestHandler<UpdateMaterijalCommand, Materijal?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMaterijalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Materijal?> Handle(UpdateMaterijalCommand request, CancellationToken cancellationToken)
        {
            var materijal = _unitOfWork.Materijali.GetById(request.Id);

            if (materijal == null)
                return Task.FromResult<Materijal?>(null);

            materijal.Naziv = request.Dto.Naziv;
            materijal.Tip = request.Dto.Tip;

            _unitOfWork.Materijali.Update(materijal);
            _unitOfWork.SaveChanges();

            return Task.FromResult<Materijal?>(materijal);
        }
    }
}
