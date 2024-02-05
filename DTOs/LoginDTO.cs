namespace Labiofam.Models;

/// <summary>
/// Esquema de inicio de sesión de la aplicación.
/// </summary>
public class LoginDTO
{
    /// <summary>
    /// Nombre del usuario (si el nombre no es nulo el email tiene que ser nulo).
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Email del usuario (si el email no es nulo el nombre tiene que ser nulo).
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Contraseña del usuario.
    /// </summary>
    public string? Password { get; set; }
}