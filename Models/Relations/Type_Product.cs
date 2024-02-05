using System.Text.Json.Serialization;

namespace Labiofam.Models;

/// <summary>
/// Entidad de la relación entre un tipo y precio con su producto relativo.
/// </summary>
public class Type_Product : IRelationDTO
{
    /// <summary>
    /// Id de la entidad Type_Price.
    /// </summary>
    public Guid Id1 { get; set; }
    /// <summary>
    /// Id del producto.
    /// </summary>
    public Guid Id2 { get; set; }

    /// <summary>
    /// Propiedad de navegación hacia el tipo y el precio.
    /// </summary>
    [JsonIgnore]
    public virtual Type_Price? Type_Price { get; set; }
    /// <summary>
    /// Propiedad de navegación hacia el producto.
    /// </summary>
    [JsonIgnore]
    public virtual Product? Product { get; set; }
}