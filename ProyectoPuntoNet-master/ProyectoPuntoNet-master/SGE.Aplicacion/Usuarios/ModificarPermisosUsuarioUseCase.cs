using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Aplicacion.Usuarios;

public class ModificarPermisosUsuarioUseCase(IUsuarioRepository repositorio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public ModificarPermisosUsuarioResponse Ejecutar(ModificarPermisosUsuarioRequest request)
    {
        // valido que quien ejecuta sea un administrador
        var admin = repositorio.ObtenerPorId(request.IdAdmin);
        if (admin == null || !admin.EsAdministrador)
        {
            throw new AutorizacionException("Solo los administradores pueden modificar los permisos de los usuarios.");
        }

        // busco al usuario al que le quiero cambiar los permisos
        var usuarioObjetivo = repositorio.ObtenerPorId(request.IdUsuarioObjetivo);
        if (usuarioObjetivo == null)
        {
            throw new EntidadNoEncontradaException("Usuario objetivo no encontrado");
        }

        // aplico cambio usando métodos de entidad
        if (request.Asignar)
        {
            usuarioObjetivo.AsignarPermiso(request.permiso);
        } else
        {
            usuarioObjetivo.QuitarPermiso(request.permiso);
        }

        unidadDeTrabajo.Guardar();

        return new ModificarPermisosUsuarioResponse();
    }
}