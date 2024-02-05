using Labiofam.Models;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de filtrado de Usuarios con Roles.
    /// </summary>
    public class UserRoleFilterService :
        RelationFilterService<User_Role, User, Role>,
        IRelationFilter<User_Role, User, Role>
    {
        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        /// <param name="relationService">Servicio de la relación.</param>
        /// <param name="entityService1">Servicio de usuarios.</param>
        /// <param name="entityService2">Servicio de roles.</param>
        public UserRoleFilterService(
            WebDbContext webDbContext,
            IRelationService<User_Role> relationService,
            IEntityService<User> entityService1,
            IEntityService<Role> entityService2
            ) : base(webDbContext, relationService, entityService1, entityService2) { }
    }
}
