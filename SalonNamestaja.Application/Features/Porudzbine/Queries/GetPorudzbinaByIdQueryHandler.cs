using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Porudzbine.Queries
{
    public class GetPorudzbinaByIdQueryHandler : IRequestHandler<GetPorudzbinaByIdQuery, Porudzbina?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPorudzbinaByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Porudzbina?> Handle(GetPorudzbinaByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Porudzbine.GetById(request.Id));
        }
    }
}
