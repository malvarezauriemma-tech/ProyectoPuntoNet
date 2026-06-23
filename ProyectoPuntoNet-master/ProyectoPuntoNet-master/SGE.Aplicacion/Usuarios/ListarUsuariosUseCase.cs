using SGE.Aplicacion.Autorizacion;

namespace SGE.Aplicacion.Usuarios;

public class ListarUsuariosUseCase(IUsuarioRepository repositorio)
{
    public ListarUsuariosResponse Ejecutar(ListarUsuariosRequest request)
    {
        // valido que quien consulta sea administrador
        var admin = repositorio.ObtenerPorId(request.IdAdmin);
        if (admin == null || !admin.EsAdministrador)
        {
            throw new AutorizacionException("No tiene permisos para listar usuarios");
        }

        // obtener entidades del repositorio
        var usuarios = repositorio.ObtenerTodos();

        // mapeo entidades a DTO para no exponer el dominio
        var listaDTOs = usuarios.Select(u => new usuarioDTO(u.Id, u.Nombre, u.CorreoElectronico, u.EsAdministrador)).ToList();

        return new ListarUsuariosResponse(listaDTOs);
    }
}