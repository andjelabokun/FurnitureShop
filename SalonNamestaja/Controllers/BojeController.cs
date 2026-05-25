using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.Data;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BojeController : ControllerBase
{
    private readonly AppDbContext _context;

    public BojeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var boje = await _context.Boje.ToListAsync();
        return Ok(boje);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var boja = await _context.Boje.FindAsync(id);
        if (boja == null)
            return NotFound("Boja nije pronađena.");
        return Ok(boja);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BojaDto dto)
    {
        var boja = new Boja
        {
            Naziv = dto.Naziv
        };
        _context.Boje.Add(boja);
        await _context.SaveChangesAsync();
        return Ok(boja);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BojaDto dto)
    {
        var boja = await _context.Boje.FindAsync(id);
        if (boja == null)
            return NotFound("Boja nije pronađena.");
        boja.Naziv = dto.Naziv;
        await _context.SaveChangesAsync();
        return Ok(boja);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var boja = await _context.Boje.FindAsync(id);
        if (boja == null)
            return NotFound("Boja nije pronađena.");
        _context.Boje.Remove(boja);
        await _context.SaveChangesAsync();
        return Ok("Boja uspešno obrisana.");
    }
}
