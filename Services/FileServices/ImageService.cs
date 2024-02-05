namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de Imágenes.
    /// </summary>
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="environment">Entorno de desarrollo actual.</param>
        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Sube al servidor la imagen dada.
        /// </summary>
        /// <param name="file">Formulario con los datos de la imagen.</param>
        /// <returns>Nombre de la imagen dentro del repositorio de imágenes.</returns>
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(_environment.ContentRootPath, "ImagesRepository", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        /// <summary>
        /// Obtiene la imagen desde el servidor.
        /// </summary>
        /// <param name="fileName">Nombre de la imagen en el repositorio de imágenes</param>
        /// <returns>La lectura en bytes de la imagen solicitada.</returns>
        public async Task<byte[]> GetImageAsync(string fileName)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "ImagesRepository", fileName);

            return await File.ReadAllBytesAsync(filePath);
        }
    }
}