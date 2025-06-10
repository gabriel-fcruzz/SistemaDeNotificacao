using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaDeNotificacao.Data;
using SistemaDeNotificacao.Models;

namespace SistemaDeNotificacao.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Evento> Eventos { get; set; }
        public List<Notificacao> Notificacoes { get; set; }

        public async Task OnGetAsync()
        {
            Eventos = await _context.Eventos.OrderByDescending(e => e.DataEvento).ToListAsync();

            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            Notificacoes = await _context.Notificacoes
                .Where(n => n.UsuarioId == userId)
                .OrderByDescending(n => n.DataCriacao)
                .ToListAsync();
        }
    }
}
