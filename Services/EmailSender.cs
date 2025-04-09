using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly string _adminEmail;
    private readonly string _adminPassword;

    public EmailSender()
    {
        _adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? throw new InvalidOperationException("Chưa cấu hình email quản trị.");
        _adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? throw new InvalidOperationException("Chưa cấu hình mật khẩu quản trị.");
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using (var client = new SmtpClient("smtp.gmail.com"))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_adminEmail, _adminPassword);
            client.EnableSsl = true;
            client.Port = 587;

            using (var mailMessage = new MailMessage(_adminEmail, email))
            {
                mailMessage.Subject = subject;
                mailMessage.Body = htmlMessage;
                mailMessage.IsBodyHtml = true;

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Có lỗi SMTP xảy ra: " + ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Có lỗi chung xảy ra: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
