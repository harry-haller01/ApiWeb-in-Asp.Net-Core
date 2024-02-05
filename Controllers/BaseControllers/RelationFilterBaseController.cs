using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de los filtros de las relaciones.
    /// </summary>
    /// <typeparam name="T">Nombre de la relación.</typeparam>
    /// <typeparam name="T1">Nombre de la primera entidad de la relación.</typeparam>
    /// <typeparam name="T2">Nombre de la segunda entidad de la relación.</typeparam>
    public abstract class RelationFilterController<T, T1, T2> : ControllerBase
    {
        private readonly IRelationFilter<T, T1, T2> _relationFilter;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="relationFilter">Servicio de filtrado.</param>
        public RelationFilterController(IRelationFilter<T, T1, T2> relationFilter)
        {
            _relationFilter = relationFilter;
        }

        /// <summary>
        /// Obtiene las entidades de tipo 2 filtradas por una entidad de tipo 1.
        /// </summary>
        /// <param name="type1_id">ID de la entidad de tipo 1.</param>
        /// <returns>Respuesta HTTP 200 OK con las entidades de tipo 2.</returns>
        [HttpGet("gettype2bytype1")]
        [AllowAnonymous]
        public async Task<IActionResult> GetType2ByType1(Guid type1_id)
        {
            try
            {
                var result = await _relationFilter.GetType2ByType1Async(type1_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene las entidades de tipo 1 filtradas por una entidad de tipo 2.
        /// </summary>
        /// <param name="type2_id">ID de la entidad de tipo 2.</param>
        /// <returns>Respuesta HTTP 200 OK con las entidades de tipo 1.</returns>
        [HttpGet("gettype1bytype2")]
        [AllowAnonymous]
        public async Task<IActionResult> GetType1ByType2(Guid type2_id)
        {
            try
            {
                var result = await _relationFilter.GetType1ByType2Async(type2_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene las entidades de tipo 2 filtradas por una entidad de tipo 1 y una subcadena.
        /// </summary>
        /// <param name="substring">Subcadena a buscar en las entidades de tipo 1.</param>
        /// <returns>Respuesta HTTP 200 OK con las entidades de tipo 2.</returns>
        [HttpGet("gettype2bytype1/{substring}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetType2ByType1Substring(string substring)
        {
            try
            {
                var result = await _relationFilter.GetType2ByType1SubstringAsync(substring);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene las entidades de tipo 1 filtradas por una entidad de tipo 2 y una subcadena.
        /// </summary>
        /// <param name="substring">Subcadena a buscar en las entidades de tipo 2.</param>
        /// <returns>Respuesta HTTP 200 OK con las entidades de tipo 1.</returns>
        [HttpGet("gettype1bytype2/{substring}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetType1ByType2Substring(string substring)
        {
            try
            {
                var result = await _relationFilter.GetType1ByType2SubstringAsync(substring);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Agrega entidades de tipo 2 a una entidad de tipo 1.
        /// </summary>
        /// <param name="type1_id">ID de la entidad de tipo 1.</param>
        /// <param name="entities">Colección de entidades de tipo 2.</param>
        /// <returns>Respuesta HTTP 200 OK si se agrega correctamente.</returns>
        [HttpPost("addtype2bytype1")]
        public async Task<IActionResult> AddType2ByType1(Guid type1_id, [FromBody] ICollection<T2> entities)
        {
            try
            {
                await _relationFilter.AddType2ByType1Async(type1_id, entities);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Agrega entidades de tipo 1 a una entidad de tipo 2.
        /// </summary>
        /// <param name="type1_id">ID de la entidad de tipo 1.</param>
        /// <param name="entities">Colección de entidades de tipo 1.</param>
        /// <returns>Respuesta HTTP 200 OK si se agrega correctamente.</returns>
        [HttpPost("addtype1bytype2")]
        public async Task<IActionResult> AddType1ByType2(Guid type1_id, [FromBody] ICollection<T1> entities)
        {
            try
            {
                await _relationFilter.AddType1ByType2Async(type1_id, entities);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}