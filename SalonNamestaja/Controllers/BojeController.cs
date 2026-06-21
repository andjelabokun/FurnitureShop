using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Application.DTOs;
using SalonNamestaja.Application.Features.Boje.Commands;
using SalonNamestaja.Application.Features.Boje.Queries;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BojeController : ControllerBase
{
    private readonly IMediator _mediator;

    public BojeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var boje = await _mediator.Send(new GetAllBojeQuery());
        return Ok(boje);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var boja = await _mediator.Send(new GetBojaByIdQuery(id));

        if (boja == null)
            return NotFound("Boja nije pronađena.");

        return Ok(boja);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(BojaDto dto)
    {
        var boja = await _mediator.Send(new CreateBojaCommand(dto));
        return Ok(boja);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BojaDto dto)
    {
        var boja = await _mediator.Send(new UpdateBojaCommand(id, dto));

        if (boja == null)
            return NotFound("Boja nije pronađena.");

        return Ok(boja);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var obrisana = await _mediator.Send(new DeleteBojaCommand(id));

            if (!obrisana)
                return NotFound("Boja nije pronađena.");

            return Ok("Boja uspešno obrisana.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}