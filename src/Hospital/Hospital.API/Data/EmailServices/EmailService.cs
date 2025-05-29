using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Hospital.API.Data.EmailServices;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    
    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    
    public async Task SendEmailToMultipleAsync(List<string> toEmails, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        
        foreach (var email in toEmails)
        {
            message.To.Add(new MailboxAddress("", email));
        }
        
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = body };

        using var client = new SmtpClient();
        await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = body };

        using var client = new SmtpClient();
        await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}