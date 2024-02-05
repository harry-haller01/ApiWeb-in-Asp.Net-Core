namespace Labiofam.Models;

/// <summary>
/// Interfaz de las entidades simples.
/// </summary>
public interface IEntityDTO
{
    /// <summary>
    /// Id de la entidad.
    /// </summary>
    Guid Id { get; set; }
    /// <summary>
    /// Nombre de la instancia de la entidad.
    /// </summary>
    string? Name { get; set; }
}