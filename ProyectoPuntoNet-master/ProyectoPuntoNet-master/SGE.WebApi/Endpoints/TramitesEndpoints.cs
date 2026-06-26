using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SGE.Aplicacion.Tramites;

namespace SGE.WebApi.Endpoints;

public static class TramitesEndpoints
{
    public static void MapTramitesEndpoints(this IEndpointRouteBuilder app)
    {
        // agrupo bajo /api/tramites y etiqueto para Scalar
        var grupo = app.MapGroup("/api/tramites").WithTags("Gestión de tramites");

        // alta de tramites: post /api/tramites
        grupo.MapPost("/", (AgregarTramiteRequest request, ClaimsPrincipal user, AgregarTramiteUseCase useCase) =>
        {
            var idUsuario = ExtraerIdUsuario(user); // extraigo el ID del token del jwt
            var response = useCase.Ejecutar(request with {UsuarioId = idUsuario});
            return Results.Created($"/api/tramites/{response.Id}", response);
        }).RequireAuthorization();

        // modificacion: post /api/tramites/{id}
        grupo.MapPut("/{id:guid}", (Guid id, ModificarTramiteRequest request, ClaimsPrincipal user, ModificarTramiteUseCase useCase) =>
        {
            var idUsuario = ExtraerIdUsuario(user);
            if (id != request.Id) return Results.BadRequest("ID de URL no coincide");

            useCase.Ejecutar(request with {UsuarioId = idUsuario});
            return Results.NoContent();
        }).RequireAuthorization();

        // baja: delete /api/tramites/{id}
        grupo.MapDelete("/{id:guid}", (Guid id, ClaimsPrincipal user, EliminarTramiteUseCase useCase) =>
        {
            var idUsuario = ExtraerIdUsuario(user);
            useCase.Ejecutar(new EliminarTramiteRequest(id, idUsuario));
            return Results.NoContent();
        }).RequireAuthorization();

        // listar por expediente: get /api/expedientes/{expedienteId}/tramites
        app.MapGet("/api/expedientes/{expedienteId:guid}/tramites", (Guid expedienteId, ListarTramitesPorExpedienteUseCase useCase) =>
        {
            var response = useCase.Ejecutar(new ListarTramitesPorExpedienteRequest(expedienteId));
            return Results.Ok(response);
        });
    }

    private static Guid ExtraerIdUsuario(ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst("ID")?.Value;
        return Guid.Parse(idClaim!);
    }
}