using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestaja.Application.Features.Materijali.Queries
{
    public class GetMaterijalByIdQueryHandler : IRequestHandler<GetMaterijalByIdQuery, Materijal?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMaterijalByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Materijal?> Handle(GetMaterijalByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Materijali.GetById(request.Id));
        }
    }
}
