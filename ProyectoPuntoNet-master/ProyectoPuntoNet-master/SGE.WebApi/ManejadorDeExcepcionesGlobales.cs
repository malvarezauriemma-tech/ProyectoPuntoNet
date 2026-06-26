using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Excepcion;
using SGE.Dominio.Comun;

namespace SGE.WebApi;

public class ManejadorDeExcepcionesGlobales: IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails { Instance = httpContext.Request.Path};

        // traducimos nuestras excepciones de negocio a códigos HTTP
        if (exception is DominioException)
        {
            problemDetails.Title = "Error de regla de negocio";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = exception.Message;
        }
        else if (exception is EntidadNoEncontradaException)
        {
            problemDetails.Title = "No encontrado";
            problemDetails.Status = StatusCodes.Status404NotFound;
            problemDetails.Detail = exception.Message;
        }
        else if (exception is AutorizacionException)
        {
            problemDetails.Title = "Acceso denegado";
            problemDetails.Status = StatusCodes.Status403Forbidden;
            problemDetails.Detail = exception.Message;
        }
        else
        {
            problemDetails.Title = "Error interno";
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Detail = "Ocurrió un error inesperado";
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true; // confirmamos que manejamos la excepcion
    }
}