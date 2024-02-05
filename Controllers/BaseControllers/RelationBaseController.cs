using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de las relaciones.
    /// </summary>
    /// <typeparam name="T">Nombre de la relación.</typeparam>
    public abstract class RelationController<T> : ControllerBase
    {
        private readonly IRelationService<T> _relationService;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="relationService">Servicio de la relación.</param>
        public RelationController(IRelationService<T> relationService)
        {
            _relationService = relationService;
        }

        /// <summary>
        /// Obtiene una relación específica utilizando dos identificadores.
        /// </summary>
        /// <param name="id1">Primer identificador.</param>
        /// <param name="id2">Segundo identificador.</param>
        /// <returns>Respuesta HTTP 200 OK con la relación.</returns>
        [HttpGet("{id1}/{id2}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id1, Guid id2)
        {
            try
            {
                var relation = await _relationService.GetAsync(id1, id2);
                return Ok(relation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene una colección de relaciones limitada por un tamaño específico.
        /// </summary>
        /// <param name="size">Tamaño de la colección.</param>
        /// <returns>Colección de relaciones.</returns>
        [HttpGet("take/{size}")]
        [AllowAnonymous]
        public async Task<IActionResult> Take(int size)
        {
            try
            {
                var result = await _relationService.TakeAsync(size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Agrega una nueva relación utilizando dos identificadores.
        /// </summary>
        /// <param name="id1">Primer identificador.</param>
        /// <param name="id2">Segundo identificador.</param>
        /// <returns>Respuesta HTTP 200 OK si se agrega correctamente.</returns>
        [HttpPost("{id1}/{id2}")]
        public virtual async Task<IActionResult> Add(Guid id1, Guid id2)
        {
            try
            {
                await _relationService.AddAsync(id1, id2);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Elimina una relación existente utilizando dos identificadores.
        /// </summary>
        /// <param name="id1">Primer identificador.</param>
        /// <param name="id2">Segundo identificador.</param>
        /// <returns>Respuesta HTTP 200 OK si se elimina correctamente.</returns>
        [HttpDelete("{id1}/{id2}")]
        public async Task<IActionResult> Remove(Guid id1, Guid id2)
        {
            try
            {
                await _relationService.RemoveAsync(id1, id2);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todas las relaciones.
        /// </summary>
        /// <returns>Respuesta HTTP 200 OK con todas las relaciones.</returns>
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var relation = await _relationService.GetAllAsync();
                return Ok(relation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Elimina todas las relaciones existentes.
        /// </summary>
        /// <returns>Respuesta HTTP 200 OK si se eliminan correctamente.</returns>
        [HttpDelete("all")]
        public async Task<IActionResult> RemoveAll()
        {
            try
            {
                await _relationService.RemoveAllAsync();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}