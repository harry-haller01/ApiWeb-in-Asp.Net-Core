using Labiofam.Models;
using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de filtros de los productos/puntos de venta.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class ProductPOSFilterController : RelationFilterController<Product_POS, Product, Point_of_Sales>
    {
        private readonly IProductPOSFilter _productPOSFilter;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="relationFilter">Servicio de filtros de los productos/puntos de venta.</param>
        /// <param name="productPOSFilter">Extensión del servicio de filtros de los productos/puntos de venta</param>
        public ProductPOSFilterController(
            IRelationFilter<Product_POS, Product, Point_of_Sales> relationFilter,
            IProductPOSFilter productPOSFilter)
            : base(relationFilter)
        {
            _productPOSFilter = productPOSFilter;
        }

        /// <summary>
        /// Agrega Productos a un Punto de Venta especificando las cantidades.
        /// </summary>
        /// <param name="type2_id">ID del Punto de Venta.</param>
        /// <param name="model">Modelo de filtro para Productos y Puntos de Venta.</param>
        /// <returns>Respuesta HTTP 200 OK si se agrega correctamente.</returns>
        [HttpPost("addtype1bytype2/size")]
        public async Task<IActionResult> AddType1ByType2(Guid type2_id, Product_POSFilterDTO model)
        {
            try
            {
                await _productPOSFilter.AddType1ByType2Async(type2_id, model.Products!, model.Sizes!);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}