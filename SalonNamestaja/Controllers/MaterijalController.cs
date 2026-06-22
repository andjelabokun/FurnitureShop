using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamestaja.Application.DTOs;
using SalonNamestaja.Application.Features.Materijali.Commands;
using SalonNamestaja.Application.Features.Materijali.Queries;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaterijalController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaterijalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var materijali = await _mediator.Send(new GetAllMaterijaliQuery());
        return Ok(materijali);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var materijal = await _mediator.Send(new GetMaterijalByIdQuery(id));

        if (materijal == null)
            return NotFound("Materijal nije pronađen.");

        return Ok(materijal);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(MaterijalDto dto)
    {
        var materijal = await _mediator.Send(new CreateMaterijalCommand(dto));
        return Ok(materijal);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MaterijalDto dto)
    {
        var materijal = await _mediator.Send(new UpdateMaterijalCommand(id, dto));

        if (materijal == null)
            return NotFound("Materijal nije pronađen.");

        return Ok(materijal);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var obrisan = await _mediator.Send(new DeleteMaterijalCommand(id));

            if (!obrisan)
                return NotFound("Materijal nije pronađen.");

            return Ok("Materijal uspešno obrisan.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}