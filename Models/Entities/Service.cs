using System.ComponentModel.DataAnnotations;

namespace Labiofam.Models;

/// <summary>
/// Entidad de los servicios ofrecidos por la empresa.
/// </summary>
public class Service : IEntityDTO
{
    /// <summary>
    /// Id de la entidad.
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// Nombre del servicio.
    /// </summary>
    [StringLength(64)]
    public string? Name { get; set; }
    /// <summary>
    /// Información relevante del servicio.
    /// </summary>
    [StringLength(2048)]
    public string? Info { get; set; }
    /// <summary>
    /// Foto relacionada con el servicio.
    /// </summary>
    [StringLength(1024)]
    public string? Image { get; set; }
}