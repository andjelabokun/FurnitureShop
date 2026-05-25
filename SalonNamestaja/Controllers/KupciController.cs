using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.Data;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KupciController : ControllerBase
{
    private readonly AppDbContext _context;

    public KupciController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var kupci = await _context.Kupci.ToListAsync();
        return Ok(kupci);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var kupac = await _context.Kupci.FindAsync(id);
        if (kupac == null)
            return NotFound("Kupac nije pronađen.");
        return Ok(kupac);
    }

    [HttpPost]
    public async Task<IActionResult> Create(KupacCreateDto dto)
    {
        var kupac = new Kupac
        {
            Ime = dto.Ime,
            Prezime = dto.Prezime,
            Email = dto.Email,
            Telefon = dto.Telefon,
            TipKupca = dto.TipKupca,
            PIB = dto.PIB
        };
        _context.Kupci.Add(kupac);
        await _context.SaveChangesAsync();
        return Ok(kupac);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, KupacCreateDto dto)
    {
        var kupac = await _context.Kupci.FindAsync(id);
        if (kupac == null)
            return NotFound("Kupac nije pronađen.");
        kupac.Ime = dto.Ime;
        kupac.Prezime = dto.Prezime;
        kupac.Email = dto.Email;
        kupac.Telefon = dto.Telefon;
        kupac.TipKupca = dto.TipKupca;
        kupac.PIB = dto.PIB;
        await _context.SaveChangesAsync();
        return Ok(kupac);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var kupac = await _context.Kupci.FindAsync(id);
        if (kupac == null)
            return NotFound("Kupac nije pronađen.");
        _context.Kupci.Remove(kupac);
        await _context.SaveChangesAsync();
        return Ok("Kupac uspešno obrisan.");
    }
}
