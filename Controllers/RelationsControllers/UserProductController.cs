using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de usuarios/productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class UserProductController : RelationController<User_Product>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="relationService">Servicio de usuarios/productos.</param>
        public UserProductController(IRelationService<User_Product> relationService)
            : base(relationService) { }
    }
}