namespace Labiofam.Models;

/// <summary>
/// Interfaz de las relaciones.
/// </summary>
public interface IRelationDTO
{
    /// <summary>
    /// Id de la primera entidad de la relación.
    /// </summary>
    Guid Id1 { get; set; }
    /// <summary>
    /// Id de la segunda entidad de la relación.
    /// </summary>
    Guid Id2 { get; set; }
}