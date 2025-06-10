using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeNotificacao.Data;
using SistemaDeNotificacao.Models;

namespace SistemaDeNotificacao.Pages.EventCRUD
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Evento Evento { get; set; }

        public void OnGet()
        {
            // Apenas renderiza o formulário
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Eventos.Add(Evento);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Privacy");
        }
    }
}
