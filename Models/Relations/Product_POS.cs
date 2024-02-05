using System.Text.Json.Serialization;

namespace Labiofam.Models;

/// <summary>
/// Entidad de la relación entre un producto y un punto de venta.
/// </summary>
public class Product_POS : IRelationDTO
{
    /// <summary>
    /// Id del producto.
    /// </summary>
    public Guid Id1 { get; set; }
    /// <summary>
    /// Id del punto de venta.
    /// </summary>
    public Guid Id2 { get; set; }

    /// <summary>
    /// Propiedad de navegación hacia el punto de venta.
    /// </summary>
    [JsonIgnore]
    public virtual Point_of_Sales? Point_Of_Sales { get; set; }
    /// <summary>
    /// Propiedad de navegación hacia el producto.
    /// </summary>
    [JsonIgnore]
    public virtual Product? Product { get; set; }
    /// <summary>
    /// Cantidad de ejemplares del producto disponibles en el punto de venta.
    /// </summary>
    public int Cantidad { get; set; }
}