using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Excepcion;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Aplicacion.Tramites;

public class AgregarTramiteUseCase(ITramiteRepository tramiteRepo, IExpedienteRepository expedienteRepository, IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService estadoService, IUnidadDeTrabajo uow)
{
    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request)
    {
        // valido autorizacion con el metodo de posee permiso
        if (!autorizacionService.PoseeElPermiso(request.UsuarioId, Permiso.TramiteAlta))
        {
            throw new AutorizacionException("No tiene permiso para agregar tramites");
        }

        // valido que el expediente exista
        var expediente = expedienteRepository.ObtenerPorId(request.ExpedienteId);
        if (expediente == null)
        {
            throw new EntidadNoEncontradaException($"No existe el expediente con ID {request.ExpedienteId}");
        }

        // creo objeto de valor y entidad
        var contenido = new ContenidoTramite(request.contenido);
        var tramite = new Tramite(request.ExpedienteId, request.Etiqueta, contenido, request.UsuarioId);

        // persistir el tramite
        tramiteRepo.Agregar(tramite);

        // actualizar estado del expediente:
        estadoService.Ejecutar(request.ExpedienteId, request.UsuarioId);

        // confirmar todo en SQLite
        uow.Guardar();

        return new AgregarTramiteResponse(tramite.Id);
    }
}