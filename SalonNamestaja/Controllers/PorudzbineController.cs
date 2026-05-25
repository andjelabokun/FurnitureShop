using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.Data;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PorudzbineController : ControllerBase
{
    private readonly AppDbContext _context;

    public PorudzbineController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var porudzbine = await _context.Porudzbine
            .Include(p => p.Kupac)
            .Include(p => p.Prodavac)
            .Include(p => p.StavkePorudzbine)
            .ToListAsync();
        return Ok(porudzbine);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var porudzbina = await _context.Porudzbine
            .Include(p => p.Kupac)
            .Include(p => p.Prodavac)
            .Include(p => p.StavkePorudzbine)
            .FirstOrDefaultAsync(p => p.PorudzbinaID == id);
        if (porudzbina == null)
            return NotFound("Porudžbina nije pronađena.");
        return Ok(porudzbina);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PorudzbinaCreateDto dto)
    {
        var porudzbina = new Porudzbina
        {
            DatumVreme = DateTime.Now,
            Status = "Kreirana",
            UkupanIznos = dto.UkupanIznos,
            KupacID = dto.KupacID,
            ProdavacID = dto.ProdavacID
        };
        _context.Porudzbine.Add(porudzbina);
        await _context.SaveChangesAsync();
        return Ok(porudzbina);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PorudzbinaCreateDto dto)
    {
        var porudzbina = await _context.Porudzbine.FindAsync(id);
        if (porudzbina == null)
            return NotFound("Porudžbina nije pronađena.");
        porudzbina.Status = dto.Status;
        porudzbina.UkupanIznos = dto.UkupanIznos;
        porudzbina.KupacID = dto.KupacID;
        porudzbina.ProdavacID = dto.ProdavacID;
        await _context.SaveChangesAsync();
        return Ok(porudzbina);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var porudzbina = await _context.Porudzbine.FindAsync(id);
        if (porudzbina == null)
            return NotFound("Porudžbina nije pronađena.");
        _context.Porudzbine.Remove(porudzbina);
        await _context.SaveChangesAsync();
        return Ok("Porudžbina uspešno obrisana.");
    }
}