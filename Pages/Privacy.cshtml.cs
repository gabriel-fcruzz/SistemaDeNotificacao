using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaDeNotificacao.Data;
using SistemaDeNotificacao.Models;

namespace SistemaDeNotificacao.Pages
{
    [Authorize(Roles = "Admin")]
    public class PrivacyModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PrivacyModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Evento> Eventos { get; set; }

        public async Task OnGetAsync()
        {
            Eventos = await _context.Eventos
                .OrderByDescending(e => e.DataEvento)
                .ToListAsync();
        }
    }

}
