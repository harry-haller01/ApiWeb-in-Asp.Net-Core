using Labiofam.Services;
using Labiofam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de servicios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class ServiceController : EntityNoDTOController<Service>
    {
        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="entityService">Servicio de servicios.</param>
        /// <param name="entityNoDTOService">Servicio sin DTO de servicios.</param>
        public ServiceController(
            IEntityService<Service> entityService,
            IEntityNoDTOService<Service> entityNoDTOService
            ) : base(entityService, entityNoDTOService) { }
    }
}