using System;

namespace SGE.Aplicacion.Expedientes;

using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Abstracciones;

public class EliminarExpedienteUseCase(IExpedienteRepository expedienteRepo, ITramiteRepository tramiteRepo, 
    IAutorizacionService autorizacionService, IUnidadDeTrabajo uow)
{
    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest request)
    {
        if (!autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteBaja))
        {
            throw new AutorizacionException("No tiene permiso para eliminar expedientes");
        }

        // si tiene permiso, realizo eliminación en cascada
        // 1: borro tramites del expediente
        var tramitesAsociados = tramiteRepo.ObtenerPorExpedienteId(request.ExpedienteId);
        foreach (var t in tramitesAsociados)
        {
            tramiteRepo.Eliminar(t.Id);
        }

        // 2: borro expediente
        expedienteRepo.Eliminar(request.ExpedienteId);
        uow.Guardar();
        
        return new EliminarExpedienteResponse();
    }
}