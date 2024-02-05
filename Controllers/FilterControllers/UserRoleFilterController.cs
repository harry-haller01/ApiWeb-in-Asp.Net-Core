using Labiofam.Models;
using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de filtros de los usuarios/roles.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class UserRoleFilterController : RelationFilterController<User_Role, User, Role>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="relationFilter">Servicio de filtros de los usuarios/roles.</param>
        public UserRoleFilterController(IRelationFilter<User_Role, User, Role> relationFilter)
            : base(relationFilter) { }
    }
}