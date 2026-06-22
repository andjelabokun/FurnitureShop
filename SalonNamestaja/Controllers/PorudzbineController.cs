using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Application.DTOs;
using SalonNamestaja.Application.Features.Porudzbine.Commands;
using SalonNamestaja.Application.Features.Porudzbine.Queries;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PorudzbineController : ControllerBase
{
    private readonly IMediator _mediator;

    public PorudzbineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var porudzbine = await _mediator.Send(new GetAllPorudzbineQuery());
        return Ok(porudzbine);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var porudzbina = await _mediator.Send(new GetPorudzbinaByIdQuery(id));

        if (porudzbina == null)
            return NotFound("Porudžbina nije pronađena.");

        return Ok(porudzbina);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("filter")]
    public async Task<IActionResult> FilterPorudzbine(
    [FromQuery] string? pretraga,
    [FromQuery] string? status,
    [FromQuery] DateTime? datumOd,
    [FromQuery] DateTime? datumDo)
    {
        var porudzbine = await _mediator.Send(
            new GetPorudzbineFilterQuery(pretraga, status, datumOd, datumDo));

        return Ok(porudzbine);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(PorudzbinaCreateDto dto)
    {
        try
        {
            var porudzbinaId = await _mediator.Send(new CreatePorudzbinaCommand(dto));
            return Ok(new { porudzbinaId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/status")]
    public async Task<IActionResult> PromeniStatus(int id, PorudzbinaUpdateDto dto)
    {
        var porudzbina = await _mediator.Send(new PromeniStatusPorudzbineCommand(id, dto.Status));

        if (porudzbina == null)
            return NotFound("Porudžbina nije pronađena.");

        return Ok(porudzbina);
    }

    

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var obrisana = await _mediator.Send(new DeletePorudzbinaCommand(id));

        if (!obrisana)
            return NotFound("Porudžbina nije pronađena.");

        return Ok("Porudžbina uspešno obrisana.");
    }
}