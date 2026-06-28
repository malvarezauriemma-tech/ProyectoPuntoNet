using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;

namespace SGE.WebApi;

public class JwtTokenProvider(IConfiguration config) : ITokenProvider
{
    public string GenerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim("ID", usuario.Id.ToString())
        };

        // traigo clave desde appsettings.json
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // fabrico tokens
        var token = new JwtSecurityToken(issuer: config["Jwt:Issuer"], audience: config["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddHours(2), signingCredentials: creds);

        // lo convierto a string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}