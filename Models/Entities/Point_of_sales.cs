using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labiofam.Models;

/// <summary>
/// Entidad de los puntos de venta.
/// </summary>
public class Point_of_Sales : IEntityDTO
{
    /// <summary>
    /// Id de la entidad.
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// Nombre del punto de venta.
    /// </summary>
    [StringLength(128)]
    public string? Name { get; set; }
    /// <summary>
    /// Fotos del punto de venta.
    /// </summary>
    [StringLength(1024)]
    public string? Image { get; set; }
    /// <summary>
    /// Dirección del punto de venta.
    /// </summary>
    [StringLength(128)]
    public string? Address { get; set; }
    /// <summary>
    /// Municipio del punto de venta.
    /// </summary>
    [StringLength(64)]
    public string? Municipality { get; set; }
    /// <summary>
    /// Provincia del punto de venta.
    /// </summary>
    [StringLength(64)]
    public string? Province { get; set; }
    /// <summary>
    /// Latitud de las coordenadas del punto de venta.
    /// </summary>
    public double Latitude { get; set; }
    /// <summary>
    /// Longitud de las coordenadas del punto de venta.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Propiedad de navegación con los productos disponibles en el punto de venta.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Product_POS>? Available_Products { get; set; }
}