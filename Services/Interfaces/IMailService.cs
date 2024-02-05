namespace Labiofam.Services;

/// <summary>
/// Interfaz del servicio de mensajería de la aplicación.
/// </summary>
public interface IMailService
{
    /// <summary>
    /// Envía un correo electrónico de forma asincrónica.
    /// </summary>
    /// <param name="sender_name">Nombre del remitente.</param>
    /// <param name="sender_email">Correo del remitente.</param>
    /// <param name="subject">Asunto del correo electrónico.</param>
    /// <param name="message">Mensaje del correo electrónico.</param>
    Task SendMailAsync(string sender_name, string sender_email, string subject, string message);
}