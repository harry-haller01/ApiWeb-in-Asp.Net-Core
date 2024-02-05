using System.Security.Claims;
using Labiofam.Models;
using Labiofam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Security;

namespace Labiofam.Controllers
{
    /// <summary>
    /// Controlador de registro y autenticación de usuarios
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RegistrationController : ControllerBase
    {
        private readonly IEntityService<Role> _roleService;
        private readonly UserManager<User> _userService;
        private readonly IEntityDTOService<User, RegistrationDTO> _userDTOService;
        private readonly IEntityDTOService<Role, RoleDTO> _roleDTOService;
        private readonly IRelationService<User_Role> _relationService;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTService _jwtService;
        private readonly IRelationFilter<User_Role, User, Role> _relationFilter;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="roleService">Servicio de roles.</param>
        /// <param name="userService">Servicio de usuarios.</param>
        /// <param name="userDTOService">Servicio con DTO de usuarios.</param>
        /// <param name="roleDTOService">Servicio con DTO de roles.</param>
        /// <param name="relationService">Servicio de usuarios/roles.</param>
        /// <param name="signInManager">Administrador de inicio de sesión.</param>
        /// <param name="jwtService">Servicio de autenticación con JWT.</param>
        /// <param name="relationFilter">Servicio de filtrado de usuarios/roles.</param>
        public RegistrationController(
            IEntityService<Role> roleService,
            UserManager<User> userService,
            IEntityDTOService<User, RegistrationDTO> userDTOService,
            IEntityDTOService<Role, RoleDTO> roleDTOService,
            IRelationService<User_Role> relationService,
            SignInManager<User> signInManager,
            IJWTService jwtService,
            IRelationFilter<User_Role, User, Role> relationFilter)
        {
            _roleService = roleService;
            _userService = userService;
            _userDTOService = userDTOService;
            _roleDTOService = roleDTOService;
            _relationService = relationService;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _relationFilter = relationFilter;
        }

        /// <summary>
        /// Método para registrar un nuevo usuario.
        /// </summary>
        /// <param name="new_user">Datos del nuevo usuario a registrar.</param>
        /// <returns>Token de inicio de sesión.</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO new_user)
        {
            User current_user;
            var current_roles = new List<Role>();

            try
            {
                current_user = await _userDTOService.AddAsync(new_user.User!);
            }
            catch (PasswordException)
            {
                return BadRequest("The password must contain at least 8 "
                    + "characters, a lower-case alphanumeric character, an "
                    + "upper-case alphanumeric character and two unique characters");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            foreach (var role in new_user.Roles!)
            {
                try
                {
                    current_roles.Add(await _roleService.GetAsync(role.Name!));
                }
                catch (InvalidOperationException)
                {
                    current_roles.Add(await _roleDTOService.AddAsync(role));
                }
                catch (Exception ex) /////////////////////////////////////
                {
                    await _userService.DeleteAsync(current_user);
                    return BadRequest(ex.Message);
                }
            }

            foreach (var role in current_roles)
            {
                await _relationService.AddAsync(current_user.Id, role.Id);
            }

            await _signInManager.SignInAsync(current_user, isPersistent: false);

            var token = _jwtService.CreateJsonWebToken(current_user, current_roles);

            current_user.RefreshToken = token.RefreshToken;
            current_user.RefreshTokenExpirationDate = token.RefreshTokenExpirationDate;
            await _userService.UpdateAsync(current_user);

            return Ok(token);
        }
        /// <summary>
        /// Método para realizar el inicio de sesión de un usuario.
        /// </summary>
        /// <param name="login">Datos de inicio de sesión del usuario.</param>
        /// <returns>Token de inicio de sesión.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (login.Name.IsNullOrEmpty() && login.Email.IsNullOrEmpty())
                return BadRequest("Name and Email can't be null at the same time");
            else if (!login.Name.IsNullOrEmpty() && !login.Email.IsNullOrEmpty())
                return BadRequest("Only one value can be provided for Name or Email");

            User? user;

            if (!login.Name.IsNullOrEmpty())
            {
                user = await _userService.FindByNameAsync(login.Name!);
                if (user is null)
                    return BadRequest("Wrong Name");
            }
            else
            {
                user = await _userService.FindByEmailAsync(login.Email!);
                if (user is null)
                    return BadRequest("Wrong Email");
            }

            var result = await _signInManager.PasswordSignInAsync(
                    user.Name!,
                    login.Password!,
                    isPersistent: false,
                    lockoutOnFailure: false);
            if (!result.Succeeded)
                return BadRequest("Wrong Password");

            await _signInManager.SignInAsync(user, isPersistent: false);

            var roles = await _relationFilter.GetType2ByType1Async(user.Id);

            var token = _jwtService.CreateJsonWebToken(user, roles);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpirationDate = token.RefreshTokenExpirationDate;
            await _userService.UpdateAsync(user);

            return Ok(token);
        }
        /// <summary>
        /// Cierra la sesión del usuario actual.
        /// </summary>
        /// <returns>Estado de la operación de Logout.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Success");
        }
        /// <summary>
        /// Genera un nuevo token de acceso para el usuario dado.
        /// </summary>
        /// <param name="model">Token de acceso anterior y refresh token.</param>
        /// <returns>Nuevo token de sesión del usuario.</returns>
        [HttpPost("getnewaccesstoken")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenDTO model)
        {
            try
            {
                var principal = _jwtService.GetPrincipalFromJWT(model.Token!);
                if (principal is null)
                    return BadRequest("Invalid token");

                var email = principal.FindFirstValue(ClaimTypes.Email);
                var user = await _userService.FindByEmailAsync(email!);
                if (user is null || !user.RefreshToken!.Equals(model.RefreshToken)
                    || user.RefreshTokenExpirationDate <= DateTime.Now)
                {
                    return BadRequest("Invalid refresh token");
                }

                var roles = await _relationFilter.GetType2ByType1Async(user.Id);

                var result = _jwtService.CreateJsonWebToken(user, roles);
                user.RefreshToken = result.RefreshToken;
                user.RefreshTokenExpirationDate = result.RefreshTokenExpirationDate;
                await _userService.UpdateAsync(user);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los datos del usuario según su token.
        /// </summary>
        /// <param name="token">Token de sesión.</param>
        /// <returns>Objeto con el usuario y sus roles.</returns>
        [HttpGet("token/{token}")]
        public async Task<IActionResult> DataByToken(string token)
        {
            try
            {
                var principal = _jwtService.GetPrincipalFromJWT(token);
                if (principal is null)
                    return BadRequest("Invalid token");

                var email = principal.FindFirstValue(ClaimTypes.Email);
                var user = await _userService.FindByEmailAsync(email!);
                var roles = await _relationFilter.GetType2ByType1Async(user!.Id);

                return Ok(new { user, roles });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}