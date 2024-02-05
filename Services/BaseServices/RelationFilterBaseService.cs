using Labiofam.Models;
using Microsoft.EntityFrameworkCore;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de filtrado de relaciones.
    /// </summary>
    /// <typeparam name="T">Relación utilizada.</typeparam>
    /// <typeparam name="T1">Primera entidad de la relación.</typeparam>
    /// <typeparam name="T2">Segunda entidad de la relación.</typeparam>
    public abstract class RelationFilterService<T, T1, T2> : IRelationFilter<T, T1, T2>
        where T : class, IRelationDTO, new()
        where T1 : class, IEntityDTO
        where T2 : class, IEntityDTO
    {
        private readonly WebDbContext _webDbContext;
        private readonly IRelationService<T> _relationService;
        private readonly IEntityService<T1> _entityService1;
        private readonly IEntityService<T2> _entityService2;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        /// <param name="relationService">Servicio de relaciones.</param>
        /// <param name="entityService1">Servicio de la primera entidad de la relación.</param>
        /// <param name="entityService2">Servicio de la segunda entidad de la relación.</param>
        public RelationFilterService(
            WebDbContext webDbContext, IRelationService<T> relationService,
            IEntityService<T1> entityService1, IEntityService<T2> entityService2)
        {
            _webDbContext = webDbContext;
            _relationService = relationService;
            _entityService1 = entityService1;
            _entityService2 = entityService2;
        }

        /// <summary>
        /// Obtiene una lista de entidades de tipo 2 por tipo 1.
        /// </summary>
        /// <param name="id">El ID de tipo 1.</param>
        /// <returns>Una lista de entidades de tipo 2.</returns>
        public async Task<ICollection<T2>> GetType2ByType1Async(Guid id)
        {
            var type1_id = await _webDbContext.Set<T>()
                .Where(ur => ur.Id1 == id)
                .ToListAsync();
            var result = new List<T2>();
            foreach (var item in type1_id)
            {
                result.Add(await _webDbContext.FindAsync<T2>(item.Id2)
                    ?? throw new NullReferenceException("Entity not found"));
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista de entidades de tipo 1 por tipo 2.
        /// </summary>
        /// <param name="id">El ID de tipo 2.</param>
        /// <returns>Una lista de entidades de tipo 1.</returns>
        public async Task<ICollection<T1>> GetType1ByType2Async(Guid id)
        {
            var type1_id = await _webDbContext.Set<T>()
                .Where(ur => ur.Id2 == id)
                .ToListAsync();
            var result = new List<T1>();
            foreach (var item in type1_id)
            {
                result.Add(await _webDbContext.FindAsync<T1>(item.Id1)
                    ?? throw new NullReferenceException("Entidad no encotrada"));
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista de entidades de tipo 2 relacionadas con los objetos
        /// de tipo 1 cuyo nombre contenga la subcadena dada.
        /// </summary>
        /// <param name="substring">La subacadena dada.</param>
        /// <returns>Una lista de entidades de tipo 2.</returns>
        public async Task<ICollection<T2>> GetType2ByType1SubstringAsync(string substring)
        {
            var entities = await _entityService1.GetBySubstringAsync(substring);

            var result = new List<T2>();
            foreach (var entity in entities)
            {
                var relation = await _webDbContext.Set<T>()
                    .Where(x => x.Id1 == entity.Id)
                    .ToListAsync();

                foreach (var item in relation)
                {
                    if (result.Any(x => x.Id == item.Id2))
                        continue;

                    result.Add(await _webDbContext.FindAsync<T2>(item.Id2)
                        ?? throw new NullReferenceException("Entity not found"));
                }
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista de entidades de tipo 1 relacionadas con los objetos
        /// de tipo 2 cuyo nombre contenga la subcadena dada.
        /// </summary>
        /// <param name="substring">La subacadena dada.</param>
        /// <returns>Una lista de entidades de tipo 1.</returns>
        public async Task<ICollection<T1>> GetType1ByType2SubstringAsync(string substring)
        {
            var entities = await _entityService2.GetBySubstringAsync(substring);

            var result = new List<T1>();
            foreach (var entity in entities)
            {
                var relation = await _webDbContext.Set<T>()
                    .Where(x => x.Id2 == entity.Id)
                    .ToListAsync();

                foreach (var item in relation)
                {
                    if (result.Any(x => x.Id == item.Id1))
                        continue;

                    result.Add(await _webDbContext.FindAsync<T1>(item.Id1)
                        ?? throw new NullReferenceException("Entity not found"));
                }
            }
            return result;
        }

        /// <summary>
        /// Agrega entidades de tipo 2 por tipo 1.
        /// </summary>
        /// <param name="id">El ID de tipo 1.</param>
        /// <param name="entities">Las entidades de tipo 2.</param>
        public async Task AddType2ByType1Async(Guid id, ICollection<T2> entities)
        {
            foreach (var type2 in entities)
            {
                try { await _relationService.AddAsync(id, type2.Id); }
                catch { continue; }
            }
        }

        /// <summary>
        /// Agrega entidades de tipo 1 por tipo 2.
        /// </summary>
        /// <param name="id">El ID de tipo 2.</param>
        /// <param name="entities">Las entidades de tipo 1.</param>
        public async Task AddType1ByType2Async(Guid id, ICollection<T1> entities)
        {
            foreach (var type1 in entities)
            {
                try { await _relationService.AddAsync(type1.Id, id); }
                catch { continue; }
            }
        }
    }
}
