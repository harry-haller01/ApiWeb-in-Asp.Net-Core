using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de lectura de los archivos json.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class JsonController : ControllerBase
    {
        private readonly IJsonService _jsonService;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="jsonService">Servicio de lectura de archivos json.</param>
        public JsonController(IJsonService jsonService)
        {
            _jsonService = jsonService;
        }

        /// <summary>
        /// Lee el archivo provincias-municipios-cuba.json.
        /// </summary>
        /// <returns>Colección de provincias con sus respectivos municipios.</returns>
        [HttpGet("jsonreader")]
        [AllowAnonymous]
        public IActionResult JsonReader()
        {
            try
            {
                var result = _jsonService.JsonReader();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}