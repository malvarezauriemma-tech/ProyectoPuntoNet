using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Aplicacion.Expedientes;

public class AgregarExpedienteUseCase(IExpedienteRepository respositorio, IAutorizacionService autorizacionService, IUnidadDeTrabajo unidadDeTrabajo)
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
        unidadDeTrabajo.Guardar();

        return new AgregarExpedienteResponse(expediente.Id);
    }
}
