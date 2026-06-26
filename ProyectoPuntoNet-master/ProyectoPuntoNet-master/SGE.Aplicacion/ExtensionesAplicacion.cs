using Microsoft.Extensions.DependencyInjection;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Usuarios;

namespace SGE.Aplicacion;


public static class ExtensionesAplicacion
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services)
    {
        // Acá registramos cada Caso de Uso para que la API pueda "pedirlos"
        services.AddScoped<AgregarExpedienteUseCase>();
        services.AddScoped<ListarTodosLosExpedientesUseCase>();
        services.AddScoped<ObtenerExpedientePorIdUseCase>();
        services.AddScoped<ModificarCaratulaExpedienteUseCase>();
        services.AddScoped<EliminarExpedienteUseCase>();
        services.AddScoped<CambiarEstadoExpedienteUseCase>();
        
        // ... y así con los de trámites y usuarios ...
        services.AddScoped<AgregarTramiteUseCase>();
        services.AddScoped<EliminarTramiteUseCase>();
        services.AddScoped<ListarTramitesPorExpedienteUseCase>();
        services.AddScoped<ModificarTramiteUseCase>();

        services.AddScoped<EliminarUsuarioUseCase>();
        services.AddScoped<ListarUsuariosUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<ModificarMisDatosUseCase>();
        services.AddScoped<ModificarPermisosUsuarioUseCase>();
        services.AddScoped<RegistrarUsuarioUseCase>();

        return services;
    }
}