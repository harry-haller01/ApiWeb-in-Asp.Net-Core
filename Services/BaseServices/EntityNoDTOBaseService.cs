using Labiofam.Models;
using Microsoft.EntityFrameworkCore;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de entidades que no usan DTO.
    /// </summary>
    /// <typeparam name="T">Entidad utilizada.</typeparam>
    public abstract class EntityNoDTOService<T> : EntityService<T>, IEntityNoDTOService<T>
        where T : class, IEntityDTO
    {
        private readonly WebDbContext _webDbContext;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public EntityNoDTOService(WebDbContext webDbContext) : base(webDbContext)
        {
            _webDbContext = webDbContext;
        }

        /// <summary>
        /// Agrega una nueva entidad.
        /// </summary>
        /// <param name="new_entity">La entidad a agregar.</param>
        public async Task AddAsync(T new_entity)
        {
            if (await _webDbContext.Set<T>().AnyAsync(
                entity => entity.Name!.Equals(new_entity.Name)))
                throw new InvalidOperationException("The entity already exists");

            await _webDbContext.AddAsync(new_entity);
            await _webDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Agrega un nuevo ICollection de entidades.
        /// </summary>
        /// <param name="new_entities">Las entidades a agregar.</param>
        public async Task AddAsync(ICollection<T> new_entities)
        {
            foreach (var entity in new_entities)
            {
                try
                {
                    await AddAsync(entity);
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Método abstracto para editar una entidad por su ID.
        /// </summary>
        /// <param name="id">ID de la entidad a editar.</param>
        /// <param name="edited_entity">La entidad editada.</param>
        public abstract Task EditAsync(Guid id, T edited_entity);
    }
}