using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestajaAPI.DTOs;
using SalonNamestajaAPI.Features.Prodavci.Commands;
using SalonNamestajaAPI.Features.Prodavci.Queries;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdavciController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProdavciController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var prodavci = await _mediator.Send(new GetAllProdavciQuery());
        return Ok(prodavci);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var prodavac = await _mediator.Send(new GetProdavacByIdQuery(id));

        if (prodavac == null)
            return NotFound("Prodavac nije pronađen.");

        return Ok(prodavac);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(ProdavacCreateDto dto)
    {
        var prodavac = await _mediator.Send(new CreateProdavacCommand(dto));
        return Ok(prodavac);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProdavacUpdateDto dto)
    {
        var prodavac = await _mediator.Send(new UpdateProdavacCommand(id, dto));

        if (prodavac == null)
            return NotFound("Prodavac nije pronađen.");

        return Ok(prodavac);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var obrisan = await _mediator.Send(new DeleteProdavacCommand(id));

        if (!obrisan)
            return NotFound("Prodavac nije pronađen.");

        return Ok("Prodavac uspešno obrisan.");
    }
}