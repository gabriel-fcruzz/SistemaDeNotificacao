using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

public class EmailSender : IEmailSender
{
    private readonly SmtpSettings _smtpSettings;

    public EmailSender(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password),
            EnableSsl = _smtpSettings.EnableSSL
        })
        {
            var mailMessage = new MailMessage(_smtpSettings.UserName, email, subject, message);
            await client.SendMailAsync(mailMessage);
        }
    }
}

public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSSL { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
