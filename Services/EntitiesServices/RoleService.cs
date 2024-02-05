using Labiofam.Models;
using Microsoft.AspNetCore.Identity;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de Roles.
    /// </summary>
    public class RoleService : EntityDTOService<Role, RoleDTO>,
        IEntityService<Role>, IEntityDTOService<Role, RoleDTO>
    {
        private readonly RoleManager<Role> _roleManager;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        /// <param name="roleManager">Servicio de Identity para manejar los roles.</param>
        public RoleService(WebDbContext webDbContext, RoleManager<Role> roleManager)
            : base(webDbContext)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// Agrega un nuevo rol.
        /// </summary>
        /// <param name="new_role">El nuevo rol a agregar.</param>
        /// <returns>El rol agregado.</returns>
        public override async Task<Role> AddAsync(RoleDTO new_role)
        {
            if (await _roleManager.RoleExistsAsync(new_role.Name!))
                throw new InvalidOperationException("The role already exists");

            var result = new Role()
            {
                Name = new_role.Name,
                Description = new_role.Description
            };

            await _roleManager.CreateAsync(result);
            return result;
        }

        /// <summary>
        /// Agrega un nuevo ICollection de roles.
        /// </summary>
        /// <param name="new_roles">Los nuevos roles a agregar.</param>
        /// <returns>Los roles agregados.</returns>
        public override async Task<ICollection<Role>> AddAsync(ICollection<RoleDTO> new_roles)
        {
            var result = new List<Role>();
            foreach (var role in new_roles)
            {
                try
                {
                    result.Add(await AddAsync(role));
                }
                catch
                {
                    continue;
                }
            }
            return result;
        }

        /// <summary>
        /// Edita un rol por su identificador único.
        /// </summary>
        /// <param name="role_id">Identificador único del rol a editar.</param>
        /// <param name="edited_role">El rol editado.</param>
        public override async Task EditAsync(Guid role_id, RoleDTO edited_role)
        {
            var current_role = await GetAsync(role_id);
            current_role.Name = edited_role.Name;
            current_role.Description = edited_role.Description;
            await _roleManager.UpdateAsync(current_role);
        }
    }
}