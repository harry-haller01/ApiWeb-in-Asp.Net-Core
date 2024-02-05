using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Labiofam.Models;

/// <summary>
/// Entidad de los roles.
/// </summary>
public class Role : IdentityRole<Guid>, IEntityDTO
{
    /// <summary>
    /// Descripción del rol.
    /// </summary>
    [StringLength(1024)]
    public string? Description { get; set; }

    /// <summary>
    /// Propiedad de navegación hacia los usuarios que portan el rol.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<User_Role>? Users { get; set; }
}