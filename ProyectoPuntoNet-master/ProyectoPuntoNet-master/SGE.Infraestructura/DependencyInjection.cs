using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Usuarios;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Autorizacion;
using SGE.Infraestructura.Persistencia;
using SGE.Infraestructura.Repositorios;
using SGE.Infraestructura.Servicios;

namespace SGE.Infraestructura;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructura(this IServiceCollection services, IConfiguration configuration)
    {
        // configuracion de base de datos
        var connectionString = configuration.GetConnectionString("SgeDb");
        services.AddDbContext<SgeContext>(options =>
            options.UseSqlite(connectionString));

        // patron unidad de trabajo
        services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

        // repositorios reales
        services.AddScoped<IExpedienteRepository, ExpedienteRepository>();
        services.AddScoped<ITramiteRepository, TramiteRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        // servicios de seguridad y autorizacion
        services.AddScoped<IAutorizacionService, AutorizacionService>();
        services.AddScoped<IHashService, HashService>(); // registro obligatorio 
        
        // retornamos, para poder permitir el encadenamiento de metodos
        return services;
    }
}