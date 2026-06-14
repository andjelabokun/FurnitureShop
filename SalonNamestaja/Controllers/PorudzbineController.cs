using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestajaAPI.DTOs;
using SalonNamestajaAPI.Features.Porudzbine.Commands;
using SalonNamestajaAPI.Features.Porudzbine.Queries;

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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(PorudzbinaCreateDto dto)
    {
        var porudzbinaId = await _mediator.Send(new CreatePorudzbinaCommand(dto));
        return Ok(new { porudzbinaId });
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
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PorudzbinaCreateDto dto)
    {
        var porudzbina = await _mediator.Send(new UpdatePorudzbinaCommand(id, dto));

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