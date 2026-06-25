using Microsoft.Extensions.DependencyInjection;
using SGE.Aplicacion;
using SGE.Aplicacion.Usuarios;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Infraestructura.Persistencia;
using SGE.Infraestructura.Repositorios;
using SGE.Infraestructura.Servicios;

namespace SGE.Infraestructura;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructura(this IServiceCollection services)
    {
        services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
        services.AddScoped<IExpedienteRepository, ExpedienteRepository>();
        services.AddScoped<ITramiteRepository, TramiteRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IAutorizacionService, AutorizacionService>();
        // No olvides registrar el HashService para las contraseñas [5, 18]
        return services;
    }
}