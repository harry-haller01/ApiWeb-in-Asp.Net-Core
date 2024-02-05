namespace Labiofam.Services;

/// <summary>
/// Interfaz para el servicio de imágenes.
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Guarda una imagen asincrónicamente en el servidor.
    /// </summary>
    /// <param name="image">Formulario con los datos de la imagen a guardar.</param>
    /// <returns>Nombre de la imagen guardada en el repositorio de imagenes.</returns>
    Task<string> UploadImageAsync(IFormFile image);

    /// <summary>
    /// Obtiene una imagen asincrónicamente.
    /// </summary>
    /// <param name="imageName">Nombre de la imagen a obtener.</param>
    /// <returns>Imagen en forma de array de bytes.</returns>
    Task<byte[]> GetImageAsync(string imageName);
}

