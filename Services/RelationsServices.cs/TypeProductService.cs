using Labiofam.Models;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de relaciones Tipo_Precio/Producto.
    /// </summary>
    public class TypeProductService : RelationService<Type_Product>, IRelationService<Type_Product>
    {
        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public TypeProductService(WebDbContext webDbContext)
            : base(webDbContext) { }
    }
}