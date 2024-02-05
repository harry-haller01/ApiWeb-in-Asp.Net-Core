using Labiofam.Models;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de relaciones Usuario/Producto.
    /// </summary>
    public class UserProductService : RelationService<User_Product>, IRelationService<User_Product>
    {
        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public UserProductService(WebDbContext webDbContext)
            : base(webDbContext) { }
    }
}