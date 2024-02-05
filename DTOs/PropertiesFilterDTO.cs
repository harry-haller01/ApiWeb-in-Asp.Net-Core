namespace Labiofam.Models;

/// <summary>
/// Esquema de filtrado de entidades según los valores de sus atributos
/// </summary>
public class PropertiesFilterDTO
{
    /// <summary>
    /// Nombres de los atributos a filtrar.
    /// </summary>
    public ICollection<string>? Names { get; set; }
    /// <summary>
    /// Valores de dichos atributos.
    /// </summary>
    public ICollection<string>? Values { get; set; }
}