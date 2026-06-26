

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SGE.Aplicacion.Abstracciones;
using SGE.WebApi;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionWebApi
{
    public static IServiceCollection AddSeguridadJwt(this IServiceCollection services, IConfiguration configuration)
    {
        // registro del proveedor de tokens 
        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        
        // configuracion del validador de tokens
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opciones =>
            {
                opciones.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });

        // activo servicio de autorizacion
        services.AddAuthorization();
        
        return services;
    }
}