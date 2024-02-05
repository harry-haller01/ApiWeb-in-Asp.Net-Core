using System.ComponentModel.DataAnnotations;

namespace Labiofam.Models;

/// <summary>
/// Entidad de los contactos.
/// </summary>
public class Contact : IEntityDTO
{
    /// <summary>
    /// Id de la entidad.
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// Nombre del contacto.
    /// </summary>
    [StringLength(64)]
    public string? Name { get; set; }
    /// <summary>
    /// Foto del contacto.
    /// </summary>
    [StringLength(1024)]
    public string? Image { get; set; }
    /// <summary>
    /// Teléfono del contacto.
    /// </summary>
    [StringLength(32)]
    public string? Phone { get; set; }
    /// <summary>
    /// Email del contacto.
    /// </summary>
    [StringLength(128)]
    public string? Email { get; set; }
    /// <summary>
    /// Puesto de trabajo del contacto en la empresa.
    /// </summary>
    [StringLength(64)]
    public string? Occupation { get; set; }
}