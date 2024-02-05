using System.ComponentModel.DataAnnotations;

namespace Labiofam.Models;

/// <summary>
/// Esquema de respuesta a la operación de registro de usuarios.
/// </summary>
public class RegistrationRequestDTO
{
    /// <summary>
    /// Nuevo usuario.
    /// </summary>
    [Required]
    public RegistrationDTO? User { get; set; }
    /// <summary>
    /// Roles que tendrá el nuevo usuario.
    /// </summary>
    [Required]
    public ICollection<RoleDTO>? Roles { get; set; }
}