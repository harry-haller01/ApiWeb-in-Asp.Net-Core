using System.ComponentModel.DataAnnotations;

namespace Labiofam.Models;

/// <summary>
/// Esquema de producto.
/// </summary>
public class ProductDTO
{
    /// <summary>
    /// Producto a utilizar.
    /// </summary>
    [Required]
    public Product? Product { get; set; }
    /// <summary>
    /// Tipos y precios de dicho producto.
    /// </summary>
    [Required]
    public ICollection<Type_Price>? Types { get; set; }
}