using System.Net;
using System.Net.Mail;
using JobBoard.Core.DTOs;

namespace JobBoard.API.Services;

public class EmailService
{
    private readonly EmailSettings _settings;

    public EmailService(
        IConfiguration configuration)
    {
        _settings =
            configuration
            .GetSection("EmailSettings")
            .Get<EmailSettings>()!;
    }

    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string body)
    {
        using var client =
            new SmtpClient(
                _settings.SmtpServer,
                _settings.Port);

        client.Credentials =
            new NetworkCredential(
                _settings.Username,
                _settings.Password);

        client.EnableSsl = true;

        var message = new MailMessage(
            _settings.SenderEmail,
            toEmail,
            subject,
            body);

        await client.SendMailAsync(message);
    }
}