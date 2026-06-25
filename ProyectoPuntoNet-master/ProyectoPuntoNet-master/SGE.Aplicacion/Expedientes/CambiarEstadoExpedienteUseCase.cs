using System;
using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Aplicacion.Expedientes;

public class CambiarEstadoExpedienteUseCase(
    IExpedienteRepository repositorio, 
    IAutorizacionService autorizacionService,
    IUnidadDeTrabajo uow)
{
    public CambiarEstadoExpedienteResponse Ejecutar(CambiarEstadoExpedienteRequest request)
    {
        // 1. Validar Autorización (Usa el permiso de modificación)
        if (!autorizacionService.PoseeElPermiso(request.UsuarioId, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("No tiene permisos para cambiar el estado de expedientes.");
        }

        // 2. Recuperar el expediente
        var expediente = repositorio.ObtenerPorId(request.Id);
        if (expediente == null)
        {
            throw new RepositorioException($"No se encontró el expediente con ID {request.Id}");
        }

        // 3. Ejecutar el comportamiento rico de la entidad
        // Este método actualiza Estado, UsuarioUltimoCambio y FechaUltimaModificacion
        expediente.CambiarEstado(request.NuevoEstado, request.UsuarioId);

        // 4. Persistir los cambios
        uow.Guardar();

        return new CambiarEstadoExpedienteResponse();
    }
}