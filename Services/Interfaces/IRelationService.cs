namespace Labiofam.Services;

/// <summary>
/// Interfaz del servicio de relaciones.
/// </summary>
/// <typeparam name="Relation">Nombre de la relación.</typeparam>
public interface IRelationService<Relation>
{
    /// <summary>
    /// Obtiene una relación de forma asincrónica por los IDs de las entidades relacionadas.
    /// </summary>
    /// <param name="model1_id">ID del lado izquierdo de la relación</param>
    /// <param name="model2_id">ID del lado derecho de la relación</param>
    /// <returns>La relación correspondiente a los IDs</returns>
    Task<Relation> GetAsync(Guid model1_id, Guid model2_id);

    /// <summary>
    /// Obtiene una colección de relaciones de tamaño específico.
    /// </summary>
    /// <param name="size">Tamaño de la colección</param>
    /// <returns>Una colección de relaciones</returns>
    Task<IEnumerable<Relation>> TakeAsync(int size);

    /// <summary>
    /// Agrega una nueva relación de forma asincrónica.
    /// </summary>
    /// <param name="model1">ID del lado izquierdo de la relación</param>
    /// <param name="model2">ID del lado derecho de la relación</param>
    Task AddAsync(Guid model1, Guid model2);

    /// <summary>
    /// Elimina una relación de forma asincrónica por los IDs de las entidades relacionadas.
    /// </summary>
    /// <param name="model1_id">ID del lado izquierdo de la relación</param>
    /// <param name="model2_id">ID del lado derecho de la relación</param>
    Task RemoveAsync(Guid model1_id, Guid model2_id);

    /// <summary>
    /// Obtiene todas las relaciones de forma asincrónica.
    /// </summary>
    /// <returns>Una lista de todas las relaciones</returns>
    Task<ICollection<Relation>> GetAllAsync();

    /// <summary>
    /// Elimina todas las relaciones de forma asincrónica.
    /// </summary>
    Task RemoveAllAsync();
}

/// <summary>
/// Interfaz extensora del servicio de relaciones para Productos/Puntos de venta.
/// </summary>
public interface IProductPOSService
{
    /// <summary>
    /// Agrega una nueva relación entre un producto y un punto de venta de forma asincrónica.
    /// </summary>
    /// <param name="product_id">ID del producto</param>
    /// <param name="pos_id">ID del punto de venta</param>
    /// <param name="size">Tamaño</param>
    Task AddAsync(Guid product_id, Guid pos_id, int size);
}