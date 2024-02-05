using System.ComponentModel.DataAnnotations;

namespace Labiofam.Models;

/// <summary>
/// Modelo de registro de usuarios en la aplicación.
/// </summary>
public class RegistrationDTO
{
    /// <summary>
    /// Nombre del nuevo usuario.
    /// </summary>
    [Required(ErrorMessage = "Name can't be blank")]
    public string? Name { get; set; }
    /// <summary>
    /// Contraseña del nuevo usuario.
    /// </summary>
    [Required(ErrorMessage = "Password can't be blank")]
    public string? Password { get; set; }
    /// <summary>
    /// Confirmación de la contraseña.
    /// </summary>
    [Required(ErrorMessage = "Confirm_Password can't be blank")]
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
    public string? Confirm_Password { get; set; }
    /// <summary>
    /// Email del nuevo usuario.
    /// </summary>
    [Required(ErrorMessage = "Email can't be blank")]
    [EmailAddress(ErrorMessage = "Invalid email")] // esto se valida rigurosamente en el servicio de user 
    public string? Email { get; set; }
    /// <summary>
    /// Código de autenticación a través del email.
    /// </summary>
    public string? Email_Token { get; set; }
    /// <summary>
    /// Teléfono del nuevo usuario.
    /// </summary>
    public string? Phone { get; set; }
    /// <summary>
    /// Foto de perfil del nuevo usuario.
    /// </summary>
    public string? Image { get; set; }
}