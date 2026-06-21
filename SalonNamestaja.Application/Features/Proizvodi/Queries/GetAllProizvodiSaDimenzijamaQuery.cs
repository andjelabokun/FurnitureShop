using MediatR;
using SalonNamestaja.Application.DTOs;

namespace SalonNamestaja.Application.Features.Proizvodi.Queries
{
    public record GetAllProizvodiSaDimenzijamaQuery() : IRequest<List<ProizvodDto>>;
}
