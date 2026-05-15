using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Excepcion;
using SGE.Dominio.Tramites;
using SGE.Aplicacion;

public class AgregarTramiteUseCase(ITramiteRepository tramiteRepo, IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService estadoService)
{
    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request)
    {
        // valido autorizacion con el metodo de posee permiso
        if (!autorizacionService.PoseeElPermiso(request.UsuarioId, Permiso.TramiteAlta))
        {
            throw new AutorizacionException("No tiene permiso para agregar tramites");
        }

        // creo objeto de valor y entidad
        var contenido = new ContenidoTramite(request.contenido);
        var tramite = new Tramite(request.ExpedienteId, request.Etiqueta, contenido, request.UsuarioId);

        // persistir el tramite
        tramiteRepo.Agregar(tramite);

        // actualizar estado del expediente:
        estadoService.Ejecutar(request.ExpedienteId, request.UsuarioId);

        return new AgregarTramiteResponse(tramite.Id);
    }
}