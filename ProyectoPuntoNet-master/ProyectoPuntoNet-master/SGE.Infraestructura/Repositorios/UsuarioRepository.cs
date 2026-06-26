using SGE.Aplicacion.Usuarios;
using SGE.Dominio.Usuarios;
using SGE.Infraestructura.Persistencia;
using SGE.Aplicacion.Excepcion;

namespace SGE.Infraestructura.Repositorios;

public class UsuarioRepository(SgeContext context) : IUsuarioRepository
{
    public void Agregar(Usuario u) => context.Usuarios.Add(u);
    public Usuario? ObtenerPorId(Guid id) => context.Usuarios.Find(id);
    public Usuario? ObtenerPorEmail(string email) => context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == email);
    public IEnumerable<Usuario> ObtenerTodos() => context.Usuarios;
    public void Eliminar(Guid id)
    {
        var u = context.Usuarios.Find(id) ?? throw new RepositorioException("No existe el usuario");
        context.Usuarios.Remove(u);
    }
   
   public void Modificar(Usuario usuario) {} // para rastrear cambios en permisos o datos
}