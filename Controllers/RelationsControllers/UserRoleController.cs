using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de usuarios/roles.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class UserRoleController : RelationController<User_Role>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="relationService">Servicio de usuarios/roles.</param>
        public UserRoleController(IRelationService<User_Role> relationService)
            : base(relationService) { }
    }
}