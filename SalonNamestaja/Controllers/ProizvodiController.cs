using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Domain;
using SalonNamestajaAPI.Data;
using SalonNamestajaAPI.DTOs;

namespace SalonNamestajaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProizvodiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProizvodiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/proizvodi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proizvodi = await _context.Proizvodi.ToListAsync();

            return Ok(proizvodi);
        }

        // GET: api/proizvodi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var proizvod = await _context.Proizvodi.FindAsync(id);

            if (proizvod == null)
            {
                return NotFound("Proizvod nije pronađen.");
            }

            return Ok(proizvod);
        }

        // POST: api/proizvodi
        [HttpPost]
        public async Task<IActionResult> Create(ProizvodCreateDto dto)
        {
            var proizvod = new Proizvod
            {
                Naziv = dto.Naziv,
                Opis = dto.Opis,
                Cena = dto.Cena,
                StanjeNaLageru = dto.StanjeNaLageru,

                PodkategorijaID = dto.PodkategorijaId,
                MaterijalID = dto.MaterijalId,
                BojaID = dto.BojaId,
                DimenzijeID = dto.DimenzijeId,
                ProizvodjacID = dto.ProizvodjacId
            };

            _context.Proizvodi.Add(proizvod);

            await _context.SaveChangesAsync();

            return Ok(proizvod);
        }

        // PUT: api/proizvodi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProizvodCreateDto dto)
        {
            var proizvod = await _context.Proizvodi.FindAsync(id);

            if (proizvod == null)
            {
                return NotFound("Proizvod nije pronađen.");
            }

            proizvod.Naziv = dto.Naziv;
            proizvod.Opis = dto.Opis;
            proizvod.Cena = dto.Cena;
            proizvod.StanjeNaLageru = dto.StanjeNaLageru;

            proizvod.PodkategorijaID = dto.PodkategorijaId;
            proizvod.MaterijalID = dto.MaterijalId;
            proizvod.BojaID = dto.BojaId;
            proizvod.DimenzijeID = dto.DimenzijeId;
            proizvod.ProizvodjacID = dto.ProizvodjacId;

            await _context.SaveChangesAsync();

            return Ok(proizvod);
        }

        // DELETE: api/proizvodi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var proizvod = await _context.Proizvodi.FindAsync(id);

            if (proizvod == null)
            {
                return NotFound("Proizvod nije pronađen.");
            }

            _context.Proizvodi.Remove(proizvod);

            await _context.SaveChangesAsync();

            return Ok("Proizvod uspešno obrisan.");
        }
    }
}
