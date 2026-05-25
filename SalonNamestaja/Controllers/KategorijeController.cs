using Microsoft.AspNetCore.Mvc;
using SalonNamestajaAPI.Data;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KategorijeController : ControllerBase
{
    private readonly AppDbContext _context;

    public KategorijeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var kategorije = await _context.Kategorije.ToListAsync();
        return Ok(kategorije);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var kategorija = await _context.Kategorije.FindAsync(id);
        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");
        return Ok(kategorija);
    }

    [HttpPost]
    public async Task<IActionResult> Create(KategorijaCreateDto dto)
    {
        var kategorija = new Kategorija
        {
            Naziv = dto.Naziv
        };
        _context.Kategorije.Add(kategorija);
        await _context.SaveChangesAsync();
        return Ok(kategorija);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, KategorijaCreateDto dto)
    {
        var kategorija = await _context.Kategorije.FindAsync(id);
        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");
        kategorija.Naziv = dto.Naziv;
        await _context.SaveChangesAsync();
        return Ok(kategorija);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var kategorija = await _context.Kategorije.FindAsync(id);
        if (kategorija == null)
            return NotFound("Kategorija nije pronađena.");
        _context.Kategorije.Remove(kategorija);
        await _context.SaveChangesAsync();
        return Ok("Kategorija uspešno obrisana.");
    }
}