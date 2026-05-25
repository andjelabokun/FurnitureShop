using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.Data;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProizvodjaciController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProizvodjaciController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var proizvodjaci = await _context.Proizvodjaci.ToListAsync();
        return Ok(proizvodjaci);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var proizvodjac = await _context.Proizvodjaci.FindAsync(id);
        if (proizvodjac == null)
            return NotFound("Proizvođač nije pronađen.");
        return Ok(proizvodjac);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProizvodjacDto dto)
    {
        var proizvodjac = new Proizvodjac
        {
            Naziv = dto.Naziv,
            Drzava = dto.Drzava
        };
        _context.Proizvodjaci.Add(proizvodjac);
        await _context.SaveChangesAsync();
        return Ok(proizvodjac);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProizvodjacDto dto)
    {
        var proizvodjac = await _context.Proizvodjaci.FindAsync(id);
        if (proizvodjac == null)
            return NotFound("Proizvođač nije pronađen.");
        proizvodjac.Naziv = dto.Naziv;
        proizvodjac.Drzava = dto.Drzava;
        await _context.SaveChangesAsync();
        return Ok(proizvodjac);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var proizvodjac = await _context.Proizvodjaci.FindAsync(id);
        if (proizvodjac == null)
            return NotFound("Proizvođač nije pronađen.");
        _context.Proizvodjaci.Remove(proizvodjac);
        await _context.SaveChangesAsync();
        return Ok("Proizvođač uspešno obrisan.");
    }
}
