using Labiofam.Models;
using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de entidades base.
    /// </summary>
    /// <typeparam name="T">Nombre de la entidad.</typeparam>
    public abstract class EntityController<T> : ControllerBase
    {
        private readonly IEntityService<T> _entityService;

        /// <summary>
        /// Constructor del controlador
        /// </summary>
        /// <param name="entityService">Servicio de la entidad.</param>
        public EntityController(IEntityService<T> entityService)
        {
            _entityService = entityService;
        }

        /// <summary>
        /// Obtiene una entidad por su ID.
        /// </summary>
        /// <param name="id">ID de la entidad.</param>
        /// <returns>La entidad correspondiente al ID proporcionado.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var entity = await _entityService.GetAsync(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene una entidad por su nombre.
        /// </summary>
        /// <param name="name">Nombre de la entidad.</param>
        /// <returns>La entidad correspondiente al nombre proporcionado.</returns>
        [HttpGet("name/{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var entity = await _entityService.GetAsync(name);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene una cantidad específica de entidades.
        /// </summary>
        /// <param name="size">Tamaño de la colección de entidades.</param>
        /// <returns>Una colección de entidades de tamaño especificado.</returns>
        [HttpGet("take/{size}")]
        [AllowAnonymous]
        public async Task<IActionResult> Take(int size)
        {
            try
            {
                var result = await _entityService.TakeAsync(size); ;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene una página específica de entidades.
        /// </summary>
        /// <param name="size">Tamaño de la página.</param>
        /// <param name="page_number">Número de la página.</param>
        /// <returns>Una colección de entidades correspondiente a la página y tamaño especificados.</returns>
        [HttpGet("take/{size}/{page_number}")]
        [AllowAnonymous]
        public async Task<IActionResult> TakeRange(int size, int page_number)
        {
            try
            {
                var result = await _entityService.TakeRangeAsync(size, page_number);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Elimina una entidad por su ID.
        /// </summary>
        /// <param name="id">ID de la entidad a eliminar.</param>
        /// <returns>Respuesta HTTP 200 OK si se elimina correctamente.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await _entityService.RemoveAsync(id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todas las entidades.
        /// </summary>
        /// <returns>Todas las entidades.</returns>
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entitys = await _entityService.GetAllAsync();
                return Ok(entitys);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Elimina todas las entidades.
        /// </summary>
        /// <returns>Respuesta HTTP 200 OK si se eliminan correctamente.</returns>
        [HttpDelete("all")]
        public async Task<IActionResult> RemoveAll()
        {
            try
            {
                await _entityService.RemoveAllAsync();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene entidades que contienen una subcadena específica.
        /// </summary>
        /// <param name="substring">Subcadena a buscar en las entidades.</param>
        /// <returns>Entidades que contienen la subcadena especificada.</returns>
        [HttpGet("getbysubstring/{substring}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySubstring(string substring)
        {
            try
            {
                var result = await _entityService.GetBySubstringAsync(substring);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Filtra las entidades del modelo dado de acuerdo a una colección
        /// de atributos con sus respectivos valores.
        /// </summary>
        /// <param name="model">Colecciones de atributo/valor.</param>
        /// <returns>La lista de entidades filtrada.</returns>
        [HttpPost("filterbyproperties")]
        [AllowAnonymous]
        public async Task<IActionResult> FilterByProperties(PropertiesFilterDTO model)
        {
            if (model.Names.IsNullOrEmpty() || model.Values.IsNullOrEmpty())
                return BadRequest("All parameters must be not null");

            try
            {
                var result = await _entityService.PropertiesFilterAsync(model.Names!, model.Values!);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}