namespace Labiofam.Models;

/// <summary>
/// Entidad de las provincias.
/// </summary>
public class Province
{
    /// <summary>
    /// Id de la entidad.
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Nombre de la provincia.
    /// </summary>
    public string? Nombre { get; set; }
    /// <summary>
    /// Colección de municipios de acuerdo a la provincia seleccionada.
    /// </summary>
    public ICollection<string>? Municipios { get; set; }
}