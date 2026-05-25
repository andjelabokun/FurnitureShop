using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.Data;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PodKategorijeController : ControllerBase
{
    private readonly AppDbContext _context;

    public PodKategorijeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var podkategorije = await _context.PodKategorije.ToListAsync();
        return Ok(podkategorije);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var podkategorija = await _context.PodKategorije.FindAsync(id);
        if (podkategorija == null)
            return NotFound("Podkategorija nije pronađena.");
        return Ok(podkategorija);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PodkategorijaCreateDto dto)
    {
        var podkategorija = new PodKategorija
        {
            Naziv = dto.Naziv,
            KategorijaID = dto.KategorijaID
        };
        _context.PodKategorije.Add(podkategorija);
        await _context.SaveChangesAsync();
        return Ok(podkategorija);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PodkategorijaCreateDto dto)
    {
        var podkategorija = await _context.PodKategorije.FindAsync(id);
        if (podkategorija == null)
            return NotFound("Podkategorija nije pronađena.");
        podkategorija.Naziv = dto.Naziv;
        podkategorija.KategorijaID = dto.KategorijaID;
        await _context.SaveChangesAsync();
        return Ok(podkategorija);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var podkategorija = await _context.PodKategorije.FindAsync(id);
        if (podkategorija == null)
            return NotFound("Podkategorija nije pronađena.");
        _context.PodKategorije.Remove(podkategorija);
        await _context.SaveChangesAsync();
        return Ok("Podkategorija uspešno obrisana.");
    }
}
