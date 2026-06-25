using SGE.Dominio.Tramites;
using SGE.Infraestructura.Persistencia;
using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Tramites;
public class TramiteRepository : ITramiteRepository
{
    private readonly SgeContext _context;
    public TramiteRepository(SgeContext context) => _context = context;

    public void Agregar(Tramite t) => _context.Tramites.Add(t);
    public Tramite? ObtenerPorId(Guid id) => _context.Tramites.Find(id);
    public IEnumerable<Tramite> ObtenerTodos() => _context.Tramites.ToList();
    public void Eliminar(Guid id)
    {
        var t = _context.Tramites.Find(id) ?? throw new RepositorioException("No existe");
        _context.Tramites.Remove(t);
    }

   public void Modificar(Tramite tramite) { }

    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        return _context.Tramites.Where(t => t.ExpedienteId == expedienteId).ToList();
    }
}