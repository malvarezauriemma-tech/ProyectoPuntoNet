using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Aplicacion.Usuarios;

public class EliminarUsuarioUseCase(IUsuarioRepository repositorio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public EliminarUsuarioResponse Ejecutar(EliminarUsuarioRequest request)
    {
        // valido permisos del ejecutor
        var admin = repositorio.ObtenerPorId(request.IdAdmin);
        if (admin == null || !admin.EsAdministrador)
        {
            throw new AutorizacionException("No tiene permisos para eliminar usuario");
        }

        // valido que no se este eliminando a si mismo
        if (request.IdAdmin  == request.IdUsuarioEliminar)
        {
            throw new AutorizacionException("No puede eliminarse a si mismo");
        }

        // persistencia: marco para eliminar y guardo
        repositorio.Eliminar(request.IdUsuarioEliminar);
        unidadDeTrabajo.Guardar();

        return new EliminarUsuarioResponse();
    }
}