using System.Net.Cache;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Tramites;

public class ModificarTramiteUseCase(ITramiteRepository tramiteRepo, IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService estadoService, IUnidadDeTrabajo uow)
{
    public ModificarTramiteResponse Ejecutar(ModificarTramiteRequest request) {
        if (!autorizacionService.PoseeElPermiso(request.UsuarioId, Permiso.TramiteModificacion)) {
            throw new AutorizacionException("No tiene permiso para modificar tramites");
        }
        // recupero tramite existente
        var tramite = tramiteRepo.ObtenerPorId(request.Id);
        if (tramite == null) {
            throw new RepositorioException($"No se encontro el tramite con id {request.Id}");
        }

        // creo objeto de valor y modifico entidad
        var contenido = new ContenidoTramite(request.nuevoContenido);
        tramite.Modificar(request.NuevaEtiqueta, contenido, request.UsuarioId);


        // actualizo estado expediente
        estadoService.Ejecutar(tramite.ExpedienteId, request.UsuarioId);

        uow.Guardar();

        return new ModificarTramiteResponse();
    }
}