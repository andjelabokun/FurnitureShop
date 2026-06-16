using Microsoft.EntityFrameworkCore;
using SalonNamestaja.Infrastructure.Data;

namespace SalonNamestajaAPI.Services
{
    public class AutoPromenaStatusaPorudzbineService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AutoPromenaStatusaPorudzbineService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var sada = DateTime.Now;

                var porudzbine = await context.Porudzbine
                    .Where(p => p.Status == "Kreirana" || p.Status == "U obradi")
                    .ToListAsync(stoppingToken);

                foreach (var porudzbina in porudzbine)
                {
                    var prosloSekundi = (sada - porudzbina.DatumVreme).TotalSeconds;

                    if (porudzbina.Status == "Kreirana" && prosloSekundi >= 10)
                    {
                        porudzbina.Status = "U obradi";
                    }
                    else if (porudzbina.Status == "U obradi" && prosloSekundi >= 20)
                    {
                        porudzbina.Status = "Isporucena";
                    }
                }

                if (porudzbine.Any())
                {
                    await context.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}