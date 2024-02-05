using Labiofam.Models;
using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de filtros de usuarios/productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class UserProductFilterController : RelationFilterController<User_Product, User, Product>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="relationFilter">Servicio de filtros de los usuarios/productos.</param>
        public UserProductFilterController(IRelationFilter<User_Product, User, Product> relationFilter)
            : base(relationFilter) { }
    }
}