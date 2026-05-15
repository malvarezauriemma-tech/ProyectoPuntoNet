using System;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Autorizacion;

namespace SGE.Aplicacion;

public class AgregarExpedienteUseCase(IExpedienteRepository respositorio, IAutorizacionService autorizacionService)
{
    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request)
    {
        // control de acceso
        if (!autorizacionService.PoseeElPermiso(request.usuarioID, Permiso.ExpedienteAlta))
        {
            throw new AutorizacionException("Usuario no autorizado para crear expedientes");
        }

        // creo caratula nueva
        var caratula = new Caratula(request.caratula);
        // creo entidad
        var expediente = new Expediente(caratula, request.usuarioID);
        respositorio.Agregar(expediente);

        return new AgregarExpedienteResponse(expediente.Id);
    }
}
