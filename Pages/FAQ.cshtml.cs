using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeNotificacao.Services;

namespace SistemaDeNotificacao.Pages
{
    public class FAQModel : PageModel
    {
        private readonly OpenRouterService _openRouterService;

        public FAQModel(OpenRouterService openRouterService)
        {
            _openRouterService = openRouterService;
        }

        [BindProperty]
        public string Pergunta { get; set; }

        public string Resposta { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Pergunta))
            {
                ModelState.AddModelError(string.Empty, "Digite uma pergunta.");
                return Page();
            }

            Resposta = await _openRouterService.PerguntarIA(Pergunta);
            return Page();
        }
    }
}