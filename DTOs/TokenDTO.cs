using System.ComponentModel.DataAnnotations;

namespace Labiofam.Models;

/// <summary>
/// Esquema de tokenización.
/// </summary>
public class TokenDTO
{
    /// <summary>
    /// Token actual.
    /// </summary>
    [Required]
    public string? Token { get; set; }
    /// <summary>
    /// Token de actualización.
    /// </summary>
    [Required]
    public string? RefreshToken { get; set; }
}