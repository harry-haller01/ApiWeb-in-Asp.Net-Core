using Labiofam.Models;
using Microsoft.EntityFrameworkCore;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de relaciones.
    /// </summary>
    /// <typeparam name="T">Relación utilizada.</typeparam>
    public abstract class RelationService<T> : IRelationService<T>
        where T : class, IRelationDTO, new()
    {
        private readonly WebDbContext _webDbContext;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public RelationService(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        /// <summary>
        /// Obtiene una lista de relaciones con un tamaño específico.
        /// </summary>
        /// <param name="size">Tamaño de la lista.</param>
        /// <returns>La lista de relaciones.</returns>
        public async Task<IEnumerable<T>> TakeAsync(int size) =>
            await _webDbContext.Set<T>().Take(size).ToListAsync();

        /// <summary>
        /// Elimina una relación por sus IDs.
        /// Los IDs siguen el patrón de nomenclatura correspondiente
        /// (User_Role => ID1 = user_id, ID2 = role_id).
        /// </summary>
        /// <param name="id1">ID1 de la relación.</param>
        /// <param name="id2">ID2 de la relación.</param>
        public async Task RemoveAsync(Guid id1, Guid id2)
        {
            var current_relation = await GetAsync(id1, id2);
            _webDbContext.Remove(current_relation);
            await _webDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Obtiene una lista de todas las relaciones.
        /// </summary>
        /// <returns>La lista de relaciones.</returns>
        public async Task<ICollection<T>> GetAllAsync()
        {
            var relations = await _webDbContext.Set<T>().ToListAsync();
            return relations;
        }

        /// <summary>
        /// Elimina todas las relaciones.
        /// </summary>
        public async Task RemoveAllAsync()
        {
            _webDbContext.RemoveRange(_webDbContext.Set<T>());
            await _webDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método abstracto para obtener una relación por sus IDs.
        /// Los IDs siguen el patrón de nomenclatura correspondiente
        /// (User_Role => ID1 = user_id, ID2 = role_id).
        /// </summary>
        /// <param name="id1">ID1 de la relación.</param>
        /// <param name="id2">ID2 de la relación.</param>
        /// <returns>La relación encontrada.</returns>
        public async Task<T> GetAsync(Guid id1, Guid id2)
        {
            var relation = await _webDbContext.Set<T>().FirstOrDefaultAsync(
                x => x.Id1.Equals(id1) && x.Id2.Equals(id2)
            ) ?? throw new InvalidOperationException("Relation not found");
            return relation;
        }

        /// <summary>
        /// Método abstracto para agregar una nueva relación por sus IDs.
        /// Los IDs siguen el patrón de nomenclatura correspondiente
        /// (User_Role => ID1 = user_id, ID2 = role_id).
        /// </summary>
        /// <param name="id1">ID1 de la relación.</param>
        /// <param name="id2">ID2 de la relación.</param>
        public virtual async Task AddAsync(Guid id1, Guid id2)
        {
            try
            {
                await GetAsync(id1, id2);
                throw new InvalidOperationException("The relation already exists");
            }
            catch
            {
                var relation = new T
                {
                    Id1 = id1,
                    Id2 = id2
                };
                await _webDbContext.AddAsync(relation);
                await _webDbContext.SaveChangesAsync();
            }
        }
    }
}