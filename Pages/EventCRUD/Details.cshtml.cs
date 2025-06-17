using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeNotificacao.Data;
using SistemaDeNotificacao.Models;

namespace SistemaDeNotificacao.Pages.EventCRUD
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Evento Evento { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Evento = await _context.Eventos.FindAsync(id);

            if (Evento == null)
                return NotFound();

            return Page();
        }
    }
}
