using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de roles.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class RoleController : EntityDTOController<Role, RoleDTO>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="entityService">Servicio de roles.</param>
        /// <param name="entityDTOService">Servicio con DTO de roles.</param>
        public RoleController(
            IEntityService<Role> entityService,
            IEntityDTOService<Role, RoleDTO> entityDTOService
            ) : base(entityService, entityDTOService) { }
    }
}