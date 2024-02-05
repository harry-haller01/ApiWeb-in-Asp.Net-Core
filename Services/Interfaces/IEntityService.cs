namespace Labiofam.Services;

/// <summary>
/// Interfaz del servicio de entidades simples.
/// </summary>
/// <typeparam name="Entity"></typeparam>
public interface IEntityService<Entity>
{
    /// <summary>
    /// Obtiene una entidad de forma asincrónica por su ID de modelo.
    /// </summary>
    /// <param name="model_id">ID del modelo</param>
    /// <returns>La entidad correspondiente al ID de modelo</returns>
    Task<Entity> GetAsync(Guid model_id);

    /// <summary>
    /// Obtiene una entidad de forma asincrónica por su nombre.
    /// </summary>
    /// <param name="name">Nombre de la entidad</param>
    /// <returns>La entidad correspondiente al nombre</returns>
    Task<Entity> GetAsync(string name);

    /// <summary>
    /// Obtiene una colección de entidades que contengan el substring en su nombre.
    /// </summary>
    /// <param name="substring">Cadena de caracteres</param>
    /// <returns>Una colección de entidades</returns>
    Task<ICollection<Entity>> GetBySubstringAsync(string substring);

    /// <summary>
    /// Obtiene una colección de entidades de tamaño específico.
    /// </summary>
    /// <param name="size">Tamaño de la colección</param>
    /// <returns>Una colección de entidades</returns>
    Task<IEnumerable<Entity>> TakeAsync(int size);

    /// <summary>
    /// Obtiene una colección de entidades de tamaño específico
    /// para una porción dada de la colección original.
    /// </summary>
    /// <param name="size">Tamaño de la colección</param>
    /// <param name="page_number">Número de la página</param>
    /// <returns>Una colección de entidades</returns>
    Task<IEnumerable<Entity>> TakeRangeAsync(int size, int page_number);

    /// <summary>
    /// Elimina una entidad de forma asincrónica por su ID de modelo.
    /// </summary>
    /// <param name="model_id">ID del modelo</param>
    Task RemoveAsync(Guid model_id);

    /// <summary>
    /// Obtiene todas las entidades de forma asincrónica.
    /// </summary>
    /// <returns>Una lista de todas las entidades</returns>
    Task<ICollection<Entity>> GetAllAsync();

    /// <summary>
    /// Elimina todas las entidades de forma asincrónica.
    /// </summary>
    Task RemoveAllAsync();

    /// <summary>
    /// Filtra las entidades de acuerdo a dos colecciones de atributos/valores.
    /// </summary>
    /// <param name="properties_names">Nombres de los atributos a filtrar.</param>
    /// <param name="properties_values">Valores de los atributos.</param>
    /// <returns>La lista de entidades filtrada.</returns>
    Task<IEnumerable<Entity>> PropertiesFilterAsync(ICollection<string> properties_names,
        ICollection<string> properties_values);
}

/// <summary>
/// Interfaz del servicio de entidades que usan DTO.
/// </summary>
/// <typeparam name="Entity">Nombre de la entidad.</typeparam>
/// <typeparam name="DTO">Nombre del DTO.</typeparam>
public interface IEntityDTOService<Entity, DTO>
{
    /// <summary>
    /// Agrega un nuevo modelo de forma asincrónica y devuelve la entidad asociada.
    /// </summary>
    /// <param name="new_model">El nuevo modelo a agregar</param>
    /// <returns>La entidad asociada al nuevo modelo</returns>
    Task<Entity> AddAsync(DTO new_model);

    /// <summary>
    /// Agrega un ICollection de nuevos modelos de forma asincrónica y devuelve las entidades asociadas.
    /// </summary>
    /// <param name="new_models">Los nuevos modelos a agregar</param>
    /// <returns>Las entidades asociadas a los nuevos modelos</returns>
    Task<ICollection<Entity>> AddAsync(ICollection<DTO> new_models);

    /// <summary>
    /// Edita un modelo de forma asincrónica por su ID de modelo.
    /// </summary>
    /// <param name="model_id">ID del modelo</param>
    /// <param name="edited_model">El modelo editado</param>
    Task EditAsync(Guid model_id, DTO edited_model);
}

/// <summary>
/// Interfaz del servicio de entidades que no usan DTO.
/// </summary>
/// <typeparam name="Entity">Nombre de la entidad.</typeparam>
public interface IEntityNoDTOService<Entity>
{
    /// <summary>
    /// Agrega una nueva entidad de forma asincrónica.
    /// </summary>
    /// <param name="new_model">La nueva entidad a agregar</param>
    Task AddAsync(Entity new_model);

    /// <summary>
    /// Agrega un ICollection de nuevos modelos de forma asincrónica y devuelve las entidades asociadas.
    /// </summary>
    /// <param name="new_models">Los nuevos modelos a agregar</param>
    /// <returns>Las entidades asociadas a los nuevos modelos</returns>
    Task AddAsync(ICollection<Entity> new_models);

    /// <summary>
    /// Edita una entidad de forma asincrónica por su ID de modelo.
    /// </summary>
    /// <param name="model_id">ID del modelo</param>
    /// <param name="edited_model">La entidad editada</param>
    Task EditAsync(Guid model_id, Entity edited_model);
}