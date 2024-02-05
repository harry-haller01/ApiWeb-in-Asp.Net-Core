using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labiofam.Models;

/// <summary>
/// Entidad de los tipos y precios de los productos.
/// </summary>
public class Type_Price : IEntityDTO
{
    /// <summary>
    /// Id de la entidad.
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// Tipo de presentación del producto.
    /// </summary>
    public string? Type { get; set; }
    /// <summary>
    /// Capacidad de almacenamiento del producto.
    /// </summary>
    public string? Capacity { get; set; }
    /// <summary>
    /// Precio del producto.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Propiedad de navegación a la entidad Producto.
    /// </summary>
    [JsonIgnore]
    public virtual Type_Product? Product { get; set; }
    /// <summary>
    /// Propiedad fantasma para asegurar la herencia del servicio.
    /// </summary>
    [JsonIgnore]
    public string? Name { get; set; } = Guid.NewGuid().ToString();
}