using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Application.DTOs;
using SalonNamestaja.Application.Features.Proizvodjaci.Commands;
using SalonNamestaja.Application.Features.Proizvodjaci.Queries;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProizvodjaciController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProizvodjaciController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var proizvodjaci = await _mediator.Send(new GetAllProizvodjaciQuery());
        return Ok(proizvodjaci);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var proizvodjac = await _mediator.Send(new GetProizvodjacByIdQuery(id));

        if (proizvodjac == null)
            return NotFound("Proizvođač nije pronađen.");

        return Ok(proizvodjac);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(ProizvodjacDto dto)
    {
        var proizvodjac = await _mediator.Send(new CreateProizvodjacCommand(dto));
        return Ok(proizvodjac);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProizvodjacDto dto)
    {
        var proizvodjac = await _mediator.Send(new UpdateProizvodjacCommand(id, dto));

        if (proizvodjac == null)
            return NotFound("Proizvođač nije pronađen.");

        return Ok(proizvodjac);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var obrisan = await _mediator.Send(new DeleteProizvodjacCommand(id));

            if (!obrisan)
                return NotFound("Proizvođač nije pronađen.");

            return Ok("Proizvođač uspešno obrisan.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}