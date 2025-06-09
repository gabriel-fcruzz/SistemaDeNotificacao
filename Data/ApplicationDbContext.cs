using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaDeNotificacao.Models;

namespace SistemaDeNotificacao.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // Use ApplicationUser aqui
    {
        protected override void OnConfiguring
(
DbContextOptionsBuilder optionsBuilder
)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            string conn = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(conn);
        }
    }
}
