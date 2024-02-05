using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Labiofam.Models;

/// <summary>
/// Entidad de los productos.
/// </summary>
public class Product : IEntityDTO
{
    /// <summary>
    /// Id de la entidad.
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// Nombre del producto.
    /// </summary>
    [Required]
    [StringLength(128)]
    public string? Name { get; set; }
    /// <summary>
    /// Tipo dentro del que se agrupa el producto.
    /// </summary>
    [StringLength(128)]
    public string? Type_of_Product { get; set; }
    /// <summary>
    /// Foto del producto.
    /// </summary>
    [StringLength(1024)]
    public string? Image { get; set; }
    /// <summary>
    /// Descripción del producto.
    /// </summary>
    [StringLength(2048)]
    public string? Description { get; set; }
    /// <summary>
    /// Enfermedades que controla el producto.
    /// </summary>
    [StringLength(2048)]
    public string? Diseases { get; set; }
    /// <summary>
    /// Ventajas del producto.
    /// </summary>
    [StringLength(2048)]
    public string? Advantages { get; set; }

    /// <summary>
    /// Propiedad para almacenar los datos extra del producto en forma de string.
    /// </summary>
    [JsonIgnore]
    public string? DatosJson { get; set; }
    /// <summary>
    /// Resumen de otras informaciones sobre el producto.
    /// </summary>
    [NotMapped]
    public Dictionary<string, string>? Summary
    {
        get
        {
            if (string.IsNullOrEmpty(DatosJson))
                return new Dictionary<string, string>();
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(DatosJson)!;
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                return new Dictionary<string, string>();
            }
        }
        set
        {
            DatosJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// Propiedad de navegación con los tipos y precios disponibles.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Type_Product>? Types { get; set; }
    /// <summary>
    /// Propiedad de navegación con los puntos de venta
    /// que tienen el producto disponible.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Product_POS>? Points_Of_Sales { get; set; }
    /// <summary>
    /// Propiedad de navegación hacia los usuarios relacionados con el producto.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<User_Product>? Users { get; set; }
}