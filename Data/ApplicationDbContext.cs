using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaDeNotificacao.Models;

namespace SistemaDeNotificacao.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // Use ApplicationUser aqui
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
