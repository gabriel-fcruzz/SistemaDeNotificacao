using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaDeNotificacao.Data;
using SistemaDeNotificacao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaDeNotificacao.Pages
{
    [Authorize(Roles = "Admin")]
    public class PrivacyModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public PrivacyModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            SelectedUserId = string.Empty;
        }

        public IList<Evento> Eventos { get; set; } = [];
        public IList<Notificacao> Notificacoes { get; set; } = [];
        public List<SelectListItem> UserOptions { get; set; } = [];

        [BindProperty(SupportsGet = true)]
        public string SelectedUserId { get; set; }

        public async Task OnGetAsync()
        {
            Eventos = await _context.Eventos
                .OrderByDescending(e => e.DataEvento)
                .ToListAsync();

            UserOptions = await _context.Users
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.Email
                })
                .ToListAsync();

            if (!string.IsNullOrEmpty(SelectedUserId))
            {
                Notificacoes = await _context.Notificacoes
                    .Where(n => n.UsuarioId == SelectedUserId)
                    .OrderByDescending(n => n.DataCriacao)
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteEventoAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(new { SelectedUserId });
        }

        public async Task<IActionResult> OnPostDeleteNotificacaoAsync(int id)
        {
            var notificacao = await _context.Notificacoes.FindAsync(id);
            if (notificacao != null)
            {
                _context.Notificacoes.Remove(notificacao);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(new { SelectedUserId });
        }

        public async Task<IActionResult> OnPostEnviarNotificacaoAsync(int id)
        {
            var notificacao = await _context.Notificacoes.FindAsync(id);

            if (notificacao != null && !notificacao.Enviado)
            {
                var usuario = await _context.Users.FindAsync(notificacao.UsuarioId);

                if (usuario != null && !string.IsNullOrEmpty(usuario.Email))
                {
                    var mensagem = $"Olá {usuario.UserName}, esta é uma notificação: {notificacao.Mensagem}";
                    await _emailSender.SendEmailAsync(usuario.Email, "Notificação Manual", mensagem);

                    notificacao.Enviado = true;
                    notificacao.DataEnvio = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage(new { SelectedUserId });
        }
    }
}
