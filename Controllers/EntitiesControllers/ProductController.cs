using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class ProductController : EntityDTOController<Product, ProductDTO>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="entityService">Servicio de productos.</param>
        /// <param name="entityDTOService">Servicio con DTO de productos.</param>
        public ProductController(
            IEntityService<Product> entityService,
            IEntityDTOService<Product, ProductDTO> entityDTOService
            ) : base(entityService, entityDTOService) { }
    }
}