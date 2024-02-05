using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de tipos y precios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class TypePriceController : EntityNoDTOController<Type_Price>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="entityService">Servicio de tipos y precios.</param>
        /// <param name="entityNoDTOService">Servicio sin DTO de tipos y precios.</param>
        public TypePriceController(
            IEntityService<Type_Price> entityService,
            IEntityNoDTOService<Type_Price> entityNoDTOService
            ) : base(entityService, entityNoDTOService) { }
    }
}