using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Usuarios;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SGE.Aplicacion.Usuarios;

namespace SGE.WebApi.Endpoints;

public static class ExpedientesEndpoints
{
    public static void MapExpedientesEndpoints(this IEndpointRouteBuilder app)
    {
        // agrupo bajo /api/expedientes y le pongo etiqueta para Scalar
        var grupo = app.MapGroup("/api/expedientes").WithTags("Gestión de expedientes");

        // listar todos: get /api/expedientes
        grupo.MapGet("/", (ListarTodosLosExpedientesUseCase useCase) =>
            Results.Ok(useCase.Ejecutar(new ListarTodosLosExpedientesRequest())));

        // obtener por id (con tramites): get /api/expedientes/{id}
        grupo.MapGet("/{id:guid}", (Guid id, ObtenerExpedientePorIdUseCase useCase) => {
            var response = useCase.Ejecutar(new ObtenerExpedientePorIdRequest(id));
            return Results.Ok(response);
        });

        // alta: post /api/expedientes
        grupo.MapPost("/", (AgregarExpedienteRequest request, ClaimsPrincipal user, AgregarExpedienteUseCase useCase) => 
        {
             var idUsuario = ExtraerIdUsuario(user); // extraigo el ID del token
             var response =  useCase.Ejecutar(request with {UsuarioId = idUsuario});
             return Results.Created($"/api/expedientes/{response.Id}", response); 
        }).RequireAuthorization();

        // modificar caratula: put /api/expedientes/{id}
        grupo.MapPut("/{id:guid}", (Guid id, ModificarCaratulaRequest request, ClaimsPrincipal user, ModificarCaratulaExpedienteUseCase useCase) =>
        {
            var idUsuario = ExtraerIdUsuario(user);
            // aseguro consistencia entre URL y Body
            if (id != request.Id) return Results.BadRequest("ID de URL no coincide con el cuerpo");

            useCase.Ejecutar(request with { UsuarioId = idUsuario});
            return Results.NoContent(); // exito pero no devuelvo datos
        }).RequireAuthorization();

        // baja en cascada: delete /api/expedientes/{id}
        grupo.MapDelete("/{id:guid}", (Guid id, ClaimsPrincipal user, EliminarExpedienteUseCase useCase) =>
        {
            var idUsuario = ExtraerIdUsuario(user);
            useCase.Ejecutar(new EliminarExpedienteRequest(id, idUsuario));
            return Results.NoContent();
        }).RequireAuthorization();
    }

    // funcion auxiliar para no repetir codigo de extraccion del ID del token
    private static Guid ExtraerIdUsuario(ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst("ID")?.Value;
        return Guid.Parse(idClaim!);
    }
}