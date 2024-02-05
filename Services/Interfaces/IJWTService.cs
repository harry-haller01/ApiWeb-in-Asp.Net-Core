using System.Security.Claims;
using Labiofam.Models;

namespace Labiofam.Services;

/// <summary>
/// Interfaz para el servicio de autenticación por JWT.
/// </summary>
public interface IJWTService
{
    /// <summary>
    /// Crea un token de inicio de sesión con los datos del usuario y sus roles.
    /// </summary>
    /// <param name="user">Usuario a autenticar.</param>
    /// <param name="roles">Roles del usuario.</param>
    /// <returns>Esquema de autenticación con el JWT dentro.</returns>
    AuthenticationDTO CreateJsonWebToken(User user, ICollection<Role> roles);
    /// <summary>
    /// Valida el token de determinado usuario.
    /// </summary>
    /// <param name="token">Token del usuario.</param>
    /// <returns>Objeto ClaimsPrincipal que representa el usuario al que autentica el token dado.</returns>
    ClaimsPrincipal? GetPrincipalFromJWT(string token);
}