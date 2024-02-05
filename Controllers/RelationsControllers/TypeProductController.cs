using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de tipos_precios/productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class TypeProductController : RelationController<Type_Product>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="relationService">Servicio de tipos_precios/productos.</param>
        public TypeProductController(IRelationService<Type_Product> relationService)
            : base(relationService) { }
    }
}