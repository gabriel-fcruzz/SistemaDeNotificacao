using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaDeNotificacao.Data;
using SistemaDeNotificacao.Models;

namespace SistemaDeNotificacao.Pages.NotificationCRUD
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Notificacao Notificacao { get; set; } = new Notificacao();

        public List<ApplicationUser> Usuarios { get; set; } = new List<ApplicationUser>();

        public async Task OnGetAsync()
        {
            Usuarios = await _userManager.Users.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recarrega lista de usuários caso o form volte com erro
                Usuarios = await _userManager.Users.ToListAsync();
                return Page();
            }

            if (string.IsNullOrEmpty(Notificacao.UsuarioId))
            {
                ModelState.AddModelError(string.Empty, "Selecione um usuário para a notificação.");
                Usuarios = await _userManager.Users.ToListAsync();
                return Page();
            }

            Notificacao.DataCriacao = DateTime.Now;
            Notificacao.Enviado = false;

            _context.Notificacoes.Add(Notificacao);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Privacy");
        }
    }
}
