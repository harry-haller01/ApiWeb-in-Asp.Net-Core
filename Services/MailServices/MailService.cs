using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio para enviar correos electrónicos.
    /// </summary>
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor de la clase MailService.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación.</param>
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Envía un correo electrónico asincrónicamente.
        /// </summary>
        /// <param name="sender_name">Nombre del remitente.</param>
        /// <param name="sender_email">Correo del remitente.</param>
        /// <param name="subject">Asunto del correo electrónico.</param>
        /// <param name="message">Mensaje del correo electrónico.</param>
        public async Task SendMailAsync(
            string sender_name, string sender_email, string subject, string message)
        {
            string email = _configuration.GetSection("EmailConfig")["MailSender"]!;
            string password = _configuration.GetSection("EmailConfig")["PasswordSender"]!;
            var smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtpClient.Authenticate(email, password);
            MimeMessage mail = new();
            mail.From.Add(new MailboxAddress(sender_name, email));
            mail.To.Add(new MailboxAddress(
                _configuration.GetSection("EmailConfig")["ServerName"],
                _configuration.GetSection("EmailConfig")["MailRecipient"]
            ));
            mail.ReplyTo.Add(new MailboxAddress(sender_name, sender_email));
            mail.Subject = subject;
            mail.Body = new TextPart("plain") { Text = message };
            await smtpClient.SendAsync(mail);
        }
    }
}