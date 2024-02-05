using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class UserController : EntityDTOController<User, RegistrationDTO>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="entityService">Servicio de usuarios.</param>
        /// <param name="entityDTOService">Servicio con DTO de usuarios.</param>
        public UserController(
            IEntityService<User> entityService,
            IEntityDTOService<User, RegistrationDTO> entityDTOService
            ) : base(entityService, entityDTOService) { }
    }
}