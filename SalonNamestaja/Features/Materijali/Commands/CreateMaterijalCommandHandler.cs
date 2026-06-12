using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Materijali.Commands
{
    public class CreateMaterijalCommandHandler : IRequestHandler<CreateMaterijalCommand, Materijal>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMaterijalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Materijal> Handle(CreateMaterijalCommand request, CancellationToken cancellationToken)
        {
            var materijal = new Materijal
            {
                Naziv = request.Dto.Naziv,
                Tip = request.Dto.Tip
            };

            _unitOfWork.Materijali.Add(materijal);
            _unitOfWork.SaveChanges();

            return Task.FromResult(materijal);
        }
    }
}
