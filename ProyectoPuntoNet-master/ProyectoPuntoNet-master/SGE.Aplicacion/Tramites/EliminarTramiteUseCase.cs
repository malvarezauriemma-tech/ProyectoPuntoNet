namespace SGE.Aplicacion.Tramites;

using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;

public class EliminarTramiteUseCase(ITramiteRepository tramiteRepo, IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizadorEstado, IUnidadDeTrabajo uow)
{
    public EliminarTramiteResponse Ejecutar(EliminarTramiteRequest request)
    {
        if (!autorizacionService.PoseeElPermiso(request.UsuarioId, Permiso.TramiteBaja))
        {
            throw new AutorizacionException("No tiene permisos para eliminar tramites");
        }

        // necesito expediente id antes de borrar para luego actualizarlo
        var tramite = tramiteRepo.ObtenerPorId(request.TramiteId);
        if (tramite == null)
        {
            throw new EntidadNoEncontradaException($"El tramite con ID: {request.TramiteId} no existe");
        }
        Guid expedienteId = tramite.ExpedienteId;

        // elimino el tramite
        tramiteRepo.Eliminar(request.TramiteId);

        // le pido al servicio que actualice el estado del expediente cosa de que, si no quedan tramites, lo ponga en RecienIniciado 
        actualizadorEstado.Ejecutar(expedienteId, request.UsuarioId);

        uow.Guardar();

        return new EliminarTramiteResponse();
    }
}