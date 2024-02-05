namespace Labiofam.Models;

/// <summary>
/// Esquema de autenticación de la aplicación.
/// </summary>
public class AuthenticationDTO
{
    /// <summary>
    /// Nombre del usuario.
    /// </summary>
    public string? Name { get; set; } = string.Empty;
    /// <summary>
    /// Email del usuario.
    /// </summary>
    public string? Email { get; set; } = string.Empty;
    /// <summary>
    /// Token de autenticación del usuario.
    /// </summary>
    public string? Token { get; set; } = string.Empty;
    /// <summary>
    /// Fecha de expiración del token de autenticación.
    /// </summary>
    public DateTime ExpirationDate { get; set; }
    /// <summary>
    /// Token de actualización del usuario.
    /// </summary>
    public string? RefreshToken { get; set; } = string.Empty;
    /// <summary>
    /// Fecha de expiración del token de actualización.
    /// </summary>
    public DateTime RefreshTokenExpirationDate { get; set; }
}