using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaDeNotificacao.Data;
using SistemaDeNotificacao.Models;

namespace SistemaDeNotificacao.Pages.NotificationCRUD
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Notificacao Notificacao { get; set; } = new Notificacao();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Notificacao = await _context.Notificacoes.FindAsync(id);

            if (Notificacao == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var notificacaoInDb = await _context.Notificacoes.AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == Notificacao.Id);

            if (notificacaoInDb == null) return NotFound();

            // Garante que o usuário da notificação não seja alterado
            Notificacao.UsuarioId = notificacaoInDb.UsuarioId;
            Notificacao.DataCriacao = notificacaoInDb.DataCriacao;
            Notificacao.Enviado = notificacaoInDb.Enviado;

            _context.Attach(Notificacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificacaoExists(Notificacao.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("/Privacy");
        }

        private bool NotificacaoExists(int id)
        {
            return _context.Notificacoes.Any(e => e.Id == id);
        }
    }
}
