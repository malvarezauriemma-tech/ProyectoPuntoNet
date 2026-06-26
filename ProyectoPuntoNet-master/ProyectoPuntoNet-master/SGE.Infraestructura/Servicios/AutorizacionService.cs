using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Usuarios;
using SGE.Dominio.Usuarios;

namespace SGE.Infraestructura.Servicios;

public class AutorizacionService(IUsuarioRepository repo) : IAutorizacionService
{
    public bool PoseeElPermiso(Guid idUsuario, Permiso permisoRequerido)
    {
        var usuario = repo.ObtenerPorId(idUsuario);
        if (usuario == null) return false;
        if (usuario.EsAdministrador) return true;

        // Regla de implicancia: ExpedienteBaja implica TramiteBaja [4]
        if (permisoRequerido == Permiso.TramiteBaja && usuario.Permisos.Contains(Permiso.ExpedienteBaja))
            return true;

        return usuario.Permisos.Contains(permisoRequerido);
    }
}