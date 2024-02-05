using System.Text.RegularExpressions;
using Labiofam.Models;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Security;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de Usuarios.
    /// </summary>
    public class UserService : EntityDTOService<User, RegistrationDTO>,
        IEntityService<User>, IEntityDTOService<User, RegistrationDTO>
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        /// <param name="userManager">Servicio de Identity para manejar usuarios.</param>
        public UserService(WebDbContext webDbContext, UserManager<User> userManager)
            : base(webDbContext)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Agrega un nuevo usuario.
        /// </summary>
        /// <param name="new_user">El nuevo usuario a agregar.</param>
        /// <returns>El usuario agregado.</returns>
        public override async Task<User> AddAsync(RegistrationDTO new_user)
        {
            new_user.Name ??= new_user.Email;

            if (await _userManager.FindByNameAsync(new_user.Name!) is not null)
                throw new ArgumentException("The user already exists");

            if (await _userManager.FindByEmailAsync(new_user.Email!) is not null)
                throw new ArgumentException("The email already exists");

            static bool IsValid(string email)
            {
                string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
                return Regex.IsMatch(email, pattern);
            }

            var user = new User()
            {
                UserName = new_user.Name,
                Email = IsValid(new_user.Email ?? throw new ArgumentNullException("Email required"))
                    ? new_user.Email : throw new ArgumentException("Email is not valid"),
                Image = new_user.Image
            };

            var result = await _userManager.CreateAsync(user,
                new_user.Password ?? throw new ArgumentNullException("Password required")
            );

            if (!result.Succeeded)
                throw new PasswordException("Password error");

            return user;
        }

        /// <summary>
        /// Agrega un nuevo ICollection de usuarios.
        /// </summary>
        /// <param name="new_users">Los nuevos usuarios a agregar.</param>
        /// <returns>Los usuarios agregados.</returns>
        public override async Task<ICollection<User>> AddAsync(ICollection<RegistrationDTO> new_users)
        {
            var result = new List<User>();
            foreach (var user in new_users)
            {
                try
                {
                    result.Add(await AddAsync(user));
                }
                catch
                {
                    continue;
                }
            }
            return result;
        }

        /// <summary>
        /// Edita un usuario por su identificador único.
        /// </summary>
        /// <param name="user_id">Identificador único del usuario a editar.</param>
        /// <param name="edited_DTO">El DTOo editado del usuario.</param>
        public override async Task EditAsync(Guid user_id, RegistrationDTO edited_DTO)
        {
            var current_user = await GetAsync(user_id);
            current_user.UserName = edited_DTO.Name;
            current_user.Image = edited_DTO.Image;

            if (edited_DTO.Confirm_Password is not null && edited_DTO.Password is not null)
                await _userManager.ChangePasswordAsync(
                    current_user, edited_DTO.Confirm_Password, edited_DTO.Password
                );

            if (edited_DTO.Email is not null && edited_DTO.Email_Token is not null)
                await _userManager.ChangeEmailAsync(
                    current_user, edited_DTO.Email, edited_DTO.Email_Token
                );

            await _userManager.UpdateAsync(current_user);
        }
    }
}