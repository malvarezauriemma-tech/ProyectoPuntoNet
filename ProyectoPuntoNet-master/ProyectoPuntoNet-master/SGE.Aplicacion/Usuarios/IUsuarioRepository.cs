namespace SGE.Aplicacion.Usuarios;

using SGE.Dominio.Usuarios;

public interface IUsuarioRepository
{
    void Agregar(Usuario usuario);
    Usuario? ObtenerPorEmail(string email);
    Usuario? ObtenerPorId(Guid id);
    IEnumerable<Usuario> ObtenerTodos(); 
    void Eliminar(Guid id);
}