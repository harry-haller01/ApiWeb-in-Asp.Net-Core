using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de archivos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "superadmin")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="imageService"></param>
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Sube al servidor la imagen dada.
        /// </summary>
        /// <param name="photo">Formulario con los datos de la imagen.</param>
        /// <returns>Nombre de la imagen dentro del repositorio de imágenes.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            try
            {
                var result = await _imageService.UploadImageAsync(photo);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Fatal error");
            }
        }
        /// <summary>
        /// Obtiene la imagen desde el servidor.
        /// </summary>
        /// <param name="fileName">Nombre de la imagen en el repositorio de imágenes</param>
        /// <returns>La lectura en bytes de la imagen solicitada.</returns>
        [HttpGet("{photoUrl}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhoto(string fileName)
        {
            try
            {
                var photo = await _imageService.GetImageAsync(fileName);
                var file = File(photo, "image/jpeg");
                return Ok(file);
            }
            catch
            {
                return BadRequest("Fatal error");
            }
        }
    }
}