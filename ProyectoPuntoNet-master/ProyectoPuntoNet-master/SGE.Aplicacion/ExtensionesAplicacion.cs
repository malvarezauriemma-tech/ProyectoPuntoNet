using Microsoft.Extensions.DependencyInjection;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Usuarios;

namespace SGE.Aplicacion;


public static class ExtensionesAplicacion
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services)
    {
        // registramos cada caso de uso para que la API pueda "pedirlos"
        // casos de uso expedientes
        services.AddScoped<AgregarExpedienteUseCase>();
        services.AddScoped<ListarTodosLosExpedientesUseCase>();
        services.AddScoped<ObtenerExpedientePorIdUseCase>();
        services.AddScoped<ModificarCaratulaExpedienteUseCase>();
        services.AddScoped<EliminarExpedienteUseCase>();
        services.AddScoped<CambiarEstadoExpedienteUseCase>();
        
        // servicios de aplicacion
        services.AddScoped<ActualizacionEstadoExpedienteService>();

        // casos de uso tramites
        services.AddScoped<AgregarTramiteUseCase>();
        services.AddScoped<EliminarTramiteUseCase>();
        services.AddScoped<ListarTramitesPorExpedienteUseCase>();
        services.AddScoped<ModificarTramiteUseCase>();

        // casos de uso usuarios
        services.AddScoped<EliminarUsuarioUseCase>();
        services.AddScoped<ListarUsuariosUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<ModificarMisDatosUseCase>();
        services.AddScoped<ModificarPermisosUsuarioUseCase>();
        services.AddScoped<RegistrarUsuarioUseCase>();

        return services;
    }
}