using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Labiofam.Models;

/// <summary>
/// Entidad de los usuarios.
/// </summary>
public class User : IdentityUser<Guid>, IEntityDTO
{
    /// <summary>
    /// Nombre del usuario.
    /// </summary>
    public string? Name { get { return UserName; } set { UserName = value; } }
    /// <summary>
    /// Foto de perfil del usuario.
    /// </summary>
    [StringLength(1024)]
    public string? Image { get; set; }
    /// <summary>
    /// Token de actualización para mantener la sesión iniciada.
    /// </summary>
    [StringLength(256)]
    public string? RefreshToken { get; set; }
    /// <summary>
    /// Fecha de expiración del token de actualización.
    /// </summary>
    public DateTime RefreshTokenExpirationDate { get; set; }

    /// <summary>
    /// Propiedad de navegación hacia los roles que porta el usuario.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<User_Role>? Roles { get; set; }
    /// <summary>
    /// Propiedad de navegación hacia los productos relacionados con el usuario.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<User_Product>? Products { get; set; }

}