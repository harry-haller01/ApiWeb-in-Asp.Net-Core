using Labiofam.Services;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de las entidades que no utilizan DTO.
    /// </summary>
    /// <typeparam name="T">Nombre de la entidad.</typeparam>
    public abstract class EntityNoDTOController<T> : EntityController<T>
    {
        private readonly IEntityNoDTOService<T> _entityNoDTOService;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="entityService">Servicio de la entidad.</param>
        /// <param name="entityNoDTOService">Servicio No-DTO de la entidad</param>
        protected EntityNoDTOController(
            IEntityService<T> entityService,
            IEntityNoDTOService<T> entityNoDTOService
            ) : base(entityService)
        {
            _entityNoDTOService = entityNoDTOService;
        }

        /// <summary>
        /// Agrega una nueva entidad sin utilizar un DTO.
        /// </summary>
        /// <param name="new_entity">Nueva entidad.</param>
        /// <returns>Respuesta HTTP 200 OK si se agrega correctamente.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(T new_entity)
        {
            try
            {
                await _entityNoDTOService.AddAsync(new_entity);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Agrega un nuevo ICollection de entidades sin utilizar DTOs.
        /// </summary>
        /// <param name="new_entities">Nuevas entidades.</param>
        /// <returns>Respuesta HTTP 200 OK si se agrega correctamente.</returns>
        [HttpPost("addmany")]
        public async Task<IActionResult> Add(ICollection<T> new_entities)
        {
            try
            {
                await _entityNoDTOService.AddAsync(new_entities);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Edita una entidad existente sin utilizar un DTO.
        /// </summary>
        /// <param name="id">ID de la entidad a editar.</param>
        /// <param name="edited_entity">Entidad editada.</param>
        /// <returns>Respuesta HTTP 200 OK si se edita correctamente.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, T edited_entity)
        {
            try
            {
                await _entityNoDTOService.EditAsync(id, edited_entity);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}