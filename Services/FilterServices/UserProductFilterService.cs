using Labiofam.Models;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de filtrado de Usuarios con Productos.
    /// </summary>
    public class UserProductFilterService :
        RelationFilterService<User_Product, User, Product>,
        IRelationFilter<User_Product, User, Product>
    {
        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        /// <param name="relationService">Servicio de la relación.</param>
        /// <param name="entityService1">Servicio de usuarios.</param>
        /// <param name="entityService2">Servicio de productos.</param>
        public UserProductFilterService(
            WebDbContext webDbContext,
            IRelationService<User_Product> relationService,
            IEntityService<User> entityService1,
            IEntityService<Product> entityService2
            ) : base(webDbContext, relationService, entityService1, entityService2) { }
    }
}