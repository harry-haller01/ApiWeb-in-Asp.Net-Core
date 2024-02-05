using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Labiofam.Models;
using Microsoft.IdentityModel.Tokens;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de autenticación con JWT.
    /// </summary>
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="configuration">Acceso al archivo de configuración.</param>
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Crea un token de inicio de sesión que contiene los datos del usuario y sus roles.
        /// </summary>
        /// <param name="user">Usuario a autenticar.</param>
        /// <param name="roles">Roles del usuario.</param>
        /// <returns>Esquema de autenticación con el JWT dentro.</returns>
        public AuthenticationDTO CreateJsonWebToken(User user, ICollection<Role> roles)
        {
            var expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration.GetSection("JWT")["EXPIRATION_MINUTES"])
                );

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Subject (user_id)
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT_id
                new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), // Issued at (time of token generation)
                new(ClaimTypes.NameIdentifier, user.Name!), // Unique name identifier of the user
                new(ClaimTypes.Email, user.Email!) // Email of the user
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name!));
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["Key"]!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenGenerator = new JwtSecurityToken(
                _configuration.GetSection("JWT")["Issuer"],
                _configuration.GetSection("JWT")["Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationDTO()
            {
                Name = user.Name,
                Email = user.Email,
                Token = token,
                ExpirationDate = expiration,
                RefreshToken = GetRefreshToken(),
                RefreshTokenExpirationDate = DateTime.Now.AddMinutes(Convert.ToDouble(
                    _configuration.GetSection("RefreshToken")["EXPIRATION_MINUTES"]))
            };
        }

        private static string GetRefreshToken()
        {
            var bytes = new byte[64];
            var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Valida el token de determinado usuario.
        /// </summary>
        /// <param name="token">Token del usuario.</param>
        /// <returns>Objeto ClaimsPrincipal que representa el usuario al que autentica el token dado.</returns>
        public ClaimsPrincipal? GetPrincipalFromJWT(string token)
        {
            var validation = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration.GetSection("JWT")["Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration.GetSection("JWT")["Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["Key"]!)),

                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                token, validation, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.CurrentCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}