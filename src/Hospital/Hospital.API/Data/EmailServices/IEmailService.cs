namespace Hospital.API.Data.EmailServices;

public interface IEmailService
{
    Task SendEmailToMultipleAsync(List<string> toEmails, string subject, string body);
    
    Task SendEmailAsync(string toEmail, string subject, string body);
}