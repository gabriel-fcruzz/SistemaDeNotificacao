using Microsoft.AspNetCore.Identity;
using SistemaDeNotificacao.Data;
using SistemaDeNotificacao.Models;
using System;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var context = services.GetRequiredService<ApplicationDbContext>();

        // 1. Criar roles  
        string[] roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // 2. Criar admin  
        string adminEmail = "admin@email.com";
        string adminPassword = "Admin@123";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, adminPassword);
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            await userManager.AddToRoleAsync(adminUser, "Admin");

        // 3. Criar eventos se não existirem  
        if (!context.Eventos.Any())
        {
            context.Eventos.AddRange(
                new Evento
                {
                    Titulo = "Passeio de Domingo",
                    Descricao = "Evento especial para os ciclistas da cidade",
                    DataEvento = new DateTime(2025, 6, 15)
                },
                new Evento
                {
                    Titulo = "Revisão Gratuita",
                    Descricao = "Traga sua bike para revisão grátis",
                    DataEvento = new DateTime(2025, 6, 20)
                }
            );
        }

        // 4. Criar notificações de exemplo (pra admin mesmo, se quiser ver funcionando)  
        if (!context.Notificacoes.Any())
        {
            context.Notificacoes.AddRange(
                new Notificacao
                {
                    UsuarioId = adminUser.Id,
                    Mensagem = "Sua bicicleta precisa ser devolvida até amanhã.",
                    DataCriacao = DateTime.Now
                },
                new Notificacao
                {
                    UsuarioId = adminUser.Id,
                    Mensagem = "Promoção especial: alugue 3 dias e pague 2!",
                    DataCriacao = DateTime.Now
                }
            );
        }

        await context.SaveChangesAsync();
    }
}
