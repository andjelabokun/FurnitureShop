using MediatR;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public record GetAllProizvodiSaDimenzijamaQuery() : IRequest<List<ProizvodDto>>;
}
