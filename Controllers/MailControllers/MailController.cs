using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador del servicio de correos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="mailService">Servicio de correos.</param>
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// Envía un correo electrónico asincrónicamente.
        /// </summary>
        /// <param name="sender_name">Nombre del remitente.</param>
        /// <param name="sender_email">Correo del remitente.</param>
        /// <param name="subject">Asunto del correo electrónico.</param>
        /// <param name="message">Mensaje del correo electrónico.</param>
        /// <returns>Respuesta HTTP 200 OK si se envía correctamente.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendMail(
            string sender_name, string sender_email, string subject, string message)
        {
            try
            {
                await _mailService.SendMailAsync(sender_name, sender_email, subject, message);
                return Ok("Success");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}