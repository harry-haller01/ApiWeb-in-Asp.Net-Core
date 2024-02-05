using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Labiofam.Models;

/// <summary>
/// Entidad de la relación entre un usuario y un rol.
/// </summary>
public class User_Role : IdentityUserRole<Guid>, IRelationDTO
{
    /// <summary>
    /// Id del usuario.
    /// </summary>
    [JsonIgnore]
    public Guid Id1 { get { return UserId; } set { UserId = value; } }
    /// <summary>
    /// Id del rol.
    /// </summary>
    [JsonIgnore]
    public Guid Id2 { get { return RoleId; } set { RoleId = value; } }

    /// <summary>
    /// Propiedad de navegación hacia el usuario.
    /// </summary>
    [JsonIgnore]
    public virtual User? User { get; set; }
    /// <summary>
    /// Propiedad de navegación hacia el rol.
    /// </summary>
    [JsonIgnore]
    public virtual Role? Role { get; set; }
}