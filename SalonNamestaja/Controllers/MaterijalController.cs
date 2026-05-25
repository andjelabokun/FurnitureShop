using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.Data;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaterijalController : ControllerBase
{
    private readonly AppDbContext _context;

    public MaterijalController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var materijali = await _context.Materijali.ToListAsync();
        return Ok(materijali);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var materijal = await _context.Materijali.FindAsync(id);
        if (materijal == null)
            return NotFound("Materijal nije pronađen.");
        return Ok(materijal);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MaterijalDto dto)
    {
        var materijal = new Materijal
        {
            Naziv = dto.Naziv,
            Tip = dto.Tip
        };
        _context.Materijali.Add(materijal);
        await _context.SaveChangesAsync();
        return Ok(materijal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MaterijalDto dto)
    {
        var materijal = await _context.Materijali.FindAsync(id);
        if (materijal == null)
            return NotFound("Materijal nije pronađen.");
        materijal.Naziv = dto.Naziv;
        materijal.Tip = dto.Tip;
        await _context.SaveChangesAsync();
        return Ok(materijal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var materijal = await _context.Materijali.FindAsync(id);
        if (materijal == null)
            return NotFound("Materijal nije pronađen.");
        _context.Materijali.Remove(materijal);
        await _context.SaveChangesAsync();
        return Ok("Materijal uspešno obrisan.");
    }
}
