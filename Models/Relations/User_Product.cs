using System.Text.Json.Serialization;

namespace Labiofam.Models;

/// <summary>
/// Entidad de la relación entre un usuario y un producto.
/// </summary>
public class User_Product : IRelationDTO
{
    /// <summary>
    /// Id del usuario.
    /// </summary>
    public Guid Id1 { get; set; }
    /// <summary>
    /// Id del producto.
    /// </summary>
    public Guid Id2 { get; set; }

    /// <summary>
    /// Propiedad de navegación hacia el usuario.
    /// </summary>
    [JsonIgnore]
    public virtual User? User { get; set; }
    /// <summary>
    /// Propiedad de navegación hacia el producto.
    /// </summary>
    [JsonIgnore]
    public virtual Product? Product { get; set; }
}