using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de contactos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class ContactController : EntityNoDTOController<Contact>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="entityService">Servicio de contactos.</param>
        /// <param name="entityNoDTOService">Servicio sin DTO de contactos.</param>
        public ContactController(
            IEntityService<Contact> entityService,
            IEntityNoDTOService<Contact> entityNoDTOService
            ) : base(entityService, entityNoDTOService) { }
    }
}