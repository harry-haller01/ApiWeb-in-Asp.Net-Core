using System.Linq.Expressions;
using Labiofam.Models;
using Microsoft.EntityFrameworkCore;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de entidades simples.
    /// </summary>
    /// <typeparam name="T">Entidad utilizada.</typeparam>
    public abstract class EntityService<T> : IEntityService<T>
        where T : class, IEntityDTO
    {
        private readonly WebDbContext _webDbContext;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public EntityService(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        /// <summary>
        /// Obtiene una entidad por su ID.
        /// </summary>
        /// <param name="id">ID de la entidad.</param>
        /// <returns>La entidad encontrada.</returns>
        public async Task<T> GetAsync(Guid id)
        {
            var current_entity = await _webDbContext.FindAsync<T>(id)
                ?? throw new InvalidOperationException("Entity not found");
            return current_entity;
        }

        /// <summary>
        /// Obtiene una entidad por su nombre.
        /// </summary>
        /// <param name="name">Nombre de la entidad.</param>
        /// <returns>La entidad encontrada.</returns>
        public async Task<T> GetAsync(string name)
        {
            var current_entity = await _webDbContext.Set<T>().FirstOrDefaultAsync(
                x => x.Name!.Equals(name)
            ) ?? throw new InvalidOperationException("Entity not found");
            return current_entity;
        }

        /// <summary>
        /// Obtiene una lista de entidades que contengan el substring en su nombre.
        /// </summary>
        /// <param name="substring">Cadena de caracteres</param>
        /// <returns>Una lista de entidades</returns>
        public async Task<ICollection<T>> GetBySubstringAsync(string substring)
        {
            var result = await _webDbContext.Set<T>()
                .Where(x => x.Name!.Contains(substring))
                .ToListAsync();
            return result;
        }

        /// <summary>
        /// Obtiene una lista de entidades con un tamaño específico.
        /// </summary>
        /// <param name="size">Tamaño de la lista.</param>
        /// <returns>La lista de entidades.</returns>
        public async Task<IEnumerable<T>> TakeAsync(int size) =>
            await _webDbContext.Set<T>().Take(size).ToListAsync();

        /// <summary>
        /// Obtiene una lista de entidades con un tamaño específico.
        /// </summary>
        /// <param name="size">Tamaño de la lista.</param>
        /// <param name="page_number">Número de página actual.</param>
        /// <returns>La lista de entidades.</returns>
        public async Task<IEnumerable<T>> TakeRangeAsync(int size, int page_number) =>
            await _webDbContext.Set<T>().Skip(size * page_number).Take(size).ToListAsync();

        /// <summary>
        /// Elimina una entidad por su ID.
        /// </summary>
        /// <param name="id">ID de la entidad a eliminar.</param>
        public async Task RemoveAsync(Guid id)
        {
            var current_entity = await GetAsync(id);
            _webDbContext.Remove(current_entity);
            await _webDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Obtiene una lista de todas las entidades.
        /// </summary>
        /// <returns>La lista de entidades.</returns>
        public async Task<ICollection<T>> GetAllAsync()
        {
            var entities = await _webDbContext.Set<T>().ToListAsync();
            return entities;
        }

        /// <summary>
        /// Elimina todas las entidades.
        /// </summary>
        public async Task RemoveAllAsync()
        {
            _webDbContext.RemoveRange(_webDbContext.Set<T>());
            await _webDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Filtra las entidades de acuerdo a una expresión lambda.
        /// No acepta atributos que no sean strings en la entidad dada.
        /// </summary>
        /// <param name="properties_names">Colección con los atributos según los cuales se filtra.</param>
        /// <param name="properties_values">Colección con los valores de los atributos a ser filtrados.</param>
        /// <returns>La lista de entidades filtrada.</returns>
        public async Task<IEnumerable<T>> PropertiesFilterAsync(ICollection<string> properties_names,
            ICollection<string> properties_values)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            Expression? body = null;

            for (int i = 0; i < properties_names.Count; i++)
            {
                var property = Expression.Property(parameter, properties_names.ElementAtOrDefault(i)
                    ?? throw new ArgumentNullException($"Null property at index: {i}"));

                try
                {
                    var value = Expression.Constant(properties_values.ElementAtOrDefault(i));
                    body = body == null ? Expression.Call(property, "Contains", Type.EmptyTypes, value)
                        : Expression.AndAlso(body, Expression.Call(property, "Contains", Type.EmptyTypes, value));
                }
                catch
                {
                    if (double.TryParse(properties_values.ElementAtOrDefault(i), out double doubleValue))
                    {
                        var value = Expression.Constant(doubleValue);
                        body = body == null ? Expression.Equal(property, value)
                            : Expression.AndAlso(body, Expression.Equal(property, value));
                    }
                    else throw new ArgumentException("Only string or double values can be passed"
                        + "as parameters");
                }
            }

            var lambda = Expression.Lambda<Func<T, bool>>(body
                ?? throw new ArgumentNullException("Some pair {property, value} is required"), parameter);

            return await _webDbContext.Set<T>().Where(lambda).ToListAsync();
        }

    }
}