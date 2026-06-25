using SGE.Aplicacion.Usuarios;
using SGE.Dominio.Usuarios;
using SGE.Infraestructura.Persistencia;
using SGE.Aplicacion.Excepcion;

namespace SGE.Infraestructura.Repositorios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SgeContext _context;
    public UsuarioRepository(SgeContext context) => _context = context;

    public void Agregar(Usuario u) => _context.Usuarios.Add(u);
    public Usuario? ObtenerPorId(Guid id) => _context.Usuarios.Find(id);
    public Usuario? ObtenerPorEmail(string email) => _context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == email);
    public List<Usuario> ObtenerTodos() => _context.Usuarios.ToList();
    public void Eliminar(Guid id)
    {
        var u = _context.Usuarios.Find(id) ?? throw new RepositorioException("No existe");
        _context.Usuarios.Remove(u);
    }
   
}