using Microsoft.AspNetCore.Mvc;
using SalonNamestajaAPI.Data;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> GetKategorije()
    {
        var kategorije = await _context.Kategorije.ToListAsync();
        return Ok(kategorije);
    }
}