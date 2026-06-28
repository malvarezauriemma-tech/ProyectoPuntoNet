using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Aplicacion.Expedientes;

public class AgregarExpedienteUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacionService, IUnidadDeTrabajo uow)
{
    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request)
    {
        // control de acceso
        if (!autorizacionService.PoseeElPermiso(request.UsuarioId, Permiso.ExpedienteAlta))
        {
            throw new AutorizacionException("Usuario no autorizado para crear expedientes");
        }

        // creo caratula nueva
        var caratula = new Caratula(request.Caratula);
        // creo entidad
        var expediente = new Expediente(caratula, request.UsuarioId);
        repositorio.Agregar(expediente);
        uow.Guardar();

        return new AgregarExpedienteResponse(expediente.Id);
    }
}
