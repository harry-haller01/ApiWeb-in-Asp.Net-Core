using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de puntos de venta.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "superadmin")]
    public class PointOfSalesController : EntityNoDTOController<Point_of_Sales>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="entityService">Servicio de puntos de venta.</param>
        /// <param name="entityNoDTOService">Servicio sin DTO de puntos de venta.</param>
        public PointOfSalesController(
            IEntityService<Point_of_Sales> entityService,
            IEntityNoDTOService<Point_of_Sales> entityNoDTOService
            ) : base(entityService, entityNoDTOService) { }
    }
}