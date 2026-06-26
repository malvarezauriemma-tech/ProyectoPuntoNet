using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SGE.Aplicacion.Usuarios;

namespace SGE.WebApi.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuariosEndpoints(this IEndpointRouteBuilder app)
    {
        // endpoint publico: login, no lleva el requireAuthorization()
        app.MapPost("/api/login", (LoginRequest request, LoginUseCase useCase) =>
        {
            var response = useCase.Ejecutar(request);
            return Results.Ok(response);
        }).WithTags("Autenticación");

        // endpoint publico: registro, permite que cualquier persona se cree una cuenta inicial
        app.MapPost("/api/usuarios", (RegistrarUsuarioRequest request, RegistrarUsuarioUseCase useCase) =>
        {
            var response = useCase.Ejecutar(request);
            return Results.Created($"/api/usuarios/{response.id}", response);
        }).WithTags("Autenticación");

        // todos los demas requieren el token
        var grupo = app.MapGroup("/api/usuarios")
            .WithTags("Gestión de usuarios")
            .RequireAuthorization(); // proteccion global

        // modificar mis datos: usuario logueado cambia su propio perfil
        grupo.MapPut("/perfil", (ModificarMisDatosRequest request, ClaimsPrincipal user, ModificarMisDatosUseCase useCase) =>
        {
            var idUsuarioToken = ExtraerIdUsuario(user);
            // el caso de uso valida que el ID del token coincida con el que se quiere modificar
            useCase.Ejecutar(request with {Id = idUsuarioToken});
            return Results.NoContent();
        });

        // opciones de administrador (el caso de uso verifica internamente que seas administrador)

        // listar todos los usuarios
        grupo.MapGet("/", (ClaimsPrincipal user, ListarUsuariosUseCase useCase) =>
        {
            var idEjecutor = ExtraerIdUsuario(user);
            var response = useCase.Ejecutar(new ListarUsuariosRequest(idEjecutor));
            return Results.Ok(response);
        });

        // eliminar usuario
        grupo.MapDelete("/{id:guid}", (Guid id, ClaimsPrincipal user, EliminarUsuarioUseCase useCase) => 
        {
            var idEjecutor = ExtraerIdUsuario(user);
            useCase.Ejecutar(new EliminarUsuarioRequest(id,idEjecutor));
            return Results.NoContent();
        });

        // modificar permisos de un usuario
        grupo.MapPatch("/{id:guid}/permisos", (Guid id, ModificarPermisosUsuarioRequest request, ClaimsPrincipal user, ModificarPermisosUsuarioUseCase useCase) =>
        {
            var idEjecutor = ExtraerIdUsuario(user);
            useCase.Ejecutar(request with {UsuarioId = id, IdEjecutor = idEjecutor});
            return Results.NoContent();
        });
    }

    private static Guid ExtraerIdUsuario(ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst("ID")?.Value;
        return Guid.Parse(idClaim!);
    }
}