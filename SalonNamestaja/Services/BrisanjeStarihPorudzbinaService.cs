using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Infrastructure.Data;

namespace SalonNamestajaAPI.Services
{
    public class BrisanjeStarihPorudzbinaService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public BrisanjeStarihPorudzbinaService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var granica = DateTime.Now.AddYears(-5);

                var starePorudzbine = await context.Porudzbine
                    .Where(p => p.DatumVreme < granica)
                    .ToListAsync(stoppingToken);

                if (starePorudzbine.Any())
                {
                    var idPorudzbina = starePorudzbine
                        .Select(p => p.PorudzbinaID)
                        .ToList();

                    var stavkeZaBrisanje = await context.StavkePorudzbine
                        .Where(s => idPorudzbina.Contains(s.PorudzbinaID))
                        .ToListAsync(stoppingToken);

                    if (stavkeZaBrisanje.Any())
                    {
                        context.StavkePorudzbine.RemoveRange(stavkeZaBrisanje);
                    }

                    context.Porudzbine.RemoveRange(starePorudzbine);

                    await context.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}