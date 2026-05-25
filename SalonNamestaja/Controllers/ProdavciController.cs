using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.Data;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdavciController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdavciController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var prodavci = await _context.Prodavci.ToListAsync();
        return Ok(prodavci);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var prodavac = await _context.Prodavci.FindAsync(id);
        if (prodavac == null)
            return NotFound("Prodavac nije pronađen.");
        return Ok(prodavac);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProdavacCreateDto dto)
    {
        var prodavac = new Prodavac
        {
            Ime = dto.Ime,
            Prezime = dto.Prezime,
            KorisnickoIme = dto.KorisnickoIme,
            Lozinka = dto.Lozinka
        };
        _context.Prodavci.Add(prodavac);
        await _context.SaveChangesAsync();
        return Ok(prodavac);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProdavacCreateDto dto)
    {
        var prodavac = await _context.Prodavci.FindAsync(id);
        if (prodavac == null)
            return NotFound("Prodavac nije pronađen.");
        prodavac.Ime = dto.Ime;
        prodavac.Prezime = dto.Prezime;
        prodavac.KorisnickoIme = dto.KorisnickoIme;
        prodavac.Lozinka = dto.Lozinka;
        await _context.SaveChangesAsync();
        return Ok(prodavac);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var prodavac = await _context.Prodavci.FindAsync(id);
        if (prodavac == null)
            return NotFound("Prodavac nije pronađen.");
        _context.Prodavci.Remove(prodavac);
        await _context.SaveChangesAsync();
        return Ok("Prodavac uspešno obrisan.");
    }
}
