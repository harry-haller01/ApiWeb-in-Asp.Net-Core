using Labiofam.Models;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de relaciones Usuario/Role.
    /// </summary>
    public class UserRoleService : RelationService<User_Role>, IRelationService<User_Role>
    {
        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public UserRoleService(WebDbContext webDbContext)
            : base(webDbContext) { }
    }
}