using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace SistemaDeNotificacao.Services
{
    public class OpenRouterService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "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";

        public OpenRouterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PerguntarIA(string pergunta)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost:5288"); // Exigência do OpenRouter
            _httpClient.DefaultRequestHeaders.Add("X-Title", "SistemaDeNotificacaoFAQ");

            var payload = new
            {
                model = "mistralai/mistral-7b-instruct:free",
                messages = new[]
                {
                    new { role = "system", content = "Você é um assistente virtual da Loja Wheels, integrado ao sistema de Notificações e Eventos.\r\n\r\nSeu papel:\r\n- Responder dúvidas frequentes sobre a loja.\r\n- Informações que você pode fornecer: horários de funcionamento, localização, regras da loja, contato e canais de atendimento.\r\n\r\nRestrições:\r\n- Não responda perguntas fora desses temas.\r\n- Caso a pergunta não seja relacionada, diga: \"Desculpe, só posso responder dúvidas sobre o funcionamento da Loja Wheels.\"\r\n\r\nSobre a Loja Wheels:\r\n- Endereço: Rua Exemplo, 123 - Centro - Cidade Y.\r\n- Horário de funcionamento: Segunda a sábado, das 9h às 18h.\r\n- Contato: (99) 9999-9999 ou contato@lojaWheels.com.br.\r\n\r\nSobre o sistema:\r\n- Você faz parte de uma aplicação, que gerencia notificações e eventos da loja.\r\n- As informações de eventos são carregadas dinamicamente do sistema.\r\n\r\nTonalidade:\r\n- Seja educado, objetivo e direto.\r\n- Respostas curtas e claras.\r\n\r\nImportante:\r\n- Não invente respostas.\r\n- Se não souber, responda que a informação não está disponível.\r\n" },
                    new { role = "user", content = pergunta }
                }
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
            response.EnsureSuccessStatusCode();

            var respostaJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(respostaJson);
            var resposta = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return resposta;
        }
    }
}
