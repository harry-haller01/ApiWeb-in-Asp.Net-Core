using System.ComponentModel.DataAnnotations;

namespace Labiofam.Models;

/// <summary>
/// Esquema de filtrado de productos con sus cantidades disponibles.
/// </summary>
public class Product_POSFilterDTO
{
    /// <summary>
    /// Lista de productos a filtrar.
    /// </summary>
    [Required]
    public ICollection<Product>? Products { get; set; }
    /// <summary>
    /// Cantidades de cada producto.
    /// </summary>
    [Required]
    public ICollection<int>? Sizes { get; set; }
}