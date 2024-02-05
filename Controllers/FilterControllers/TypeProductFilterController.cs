using Labiofam.Models;
using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de filtros de los tipos_precios/productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class TypeProductFilterController : RelationFilterController<Type_Product, Type_Price, Product>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="relationFilter">Servicio de filtros de los tipos_precios/productos.</param>
        public TypeProductFilterController(IRelationFilter<Type_Product, Type_Price, Product> relationFilter)
            : base(relationFilter) { }
    }
}