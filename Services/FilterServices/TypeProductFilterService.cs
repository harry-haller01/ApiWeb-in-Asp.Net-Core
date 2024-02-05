using Labiofam.Models;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de filtrado de Tipos y Precios con Productos
    /// </summary>
    public class TypeProductFilterService :
        RelationFilterService<Type_Product, Type_Price, Product>,
        IRelationFilter<Type_Product, Type_Price, Product>
    {
        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        /// <param name="relationService">Servicio de la relación.</param>
        /// <param name="entityService1">Servicio de tipos y precios.</param>
        /// <param name="entityService2">Servicio de productos.</param>
        public TypeProductFilterService(
            WebDbContext webDbContext,
            IRelationService<Type_Product> relationService,
            IEntityService<Type_Price> entityService1,
            IEntityService<Product> entityService2
            ) : base(webDbContext, relationService, entityService1, entityService2) { }
    }
}