using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SistemaDeNotificacao.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaDeNotificacao.Services
{
    public class NotificationEmail : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private PeriodicTimer _timer;
        private CancellationTokenSource _cts;

        public NotificationEmail(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _timer = new PeriodicTimer(TimeSpan.FromHours(1)); // roda a cada 1 hora

            _ = DoWorkAsync(_cts.Token); // dispara o loop async, não espera aqui

            return Task.CompletedTask;
        }

        private async Task DoWorkAsync(CancellationToken token)
        {
            while (await _timer.WaitForNextTickAsync(token))
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                    var hoje = DateTime.Today;

                    var notificacoes = await dbContext.Notificacoes
                        .Where(n => n.DataEnvio.Date == hoje && !n.Enviado)
                        .ToListAsync(token);

                    foreach (var notificacao in notificacoes)
                    {
                        var usuarios = await dbContext.Users.ToListAsync(token);

                        foreach (var usuario in usuarios)
                        {
                            var mensagem = $"Olá {usuario.UserName}, esta é sua notificação: {notificacao.Mensagem}";
                            await emailSender.SendEmailAsync(usuario.Email, "Notificação", mensagem);
                        }

                        notificacao.Enviado = true;
                    }

                    await dbContext.SaveChangesAsync(token);
                }
                catch (OperationCanceledException)
                {
                    // Cancelamento esperado, ignore
                }
                catch (Exception ex)
                {
                    // Aqui trate ou logue o erro apropriadamente
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_timer != null)
            {
                _cts.Cancel();

                try
                {
                    // Replace DisposeAsync with Dispose as PeriodicTimer does not support DisposeAsync  
                    _timer.Dispose();
                }
                catch { }

                _timer = null;
                _cts.Dispose();
                _cts = null;
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _cts?.Dispose();
        }
    }
}
