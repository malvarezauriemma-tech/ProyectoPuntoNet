using SGE.Dominio.Expedientes;
using SGE.Infraestructura.Persistencia;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Excepcion;


public class ExpedienteRepository : IExpedienteRepository
{
    private readonly SgeContext _context;
    public ExpedienteRepository(SgeContext context) => _context = context;

    public void Agregar(Expediente e) => _context.Expedientes.Add(e);
    public Expediente? ObtenerPorId(Guid id) => _context.Expedientes.Find(id);
    public IEnumerable<Expediente> ObtenerTodos() => _context.Expedientes.ToList();
    
    public void Eliminar(Guid id)
    {
        var e = _context.Expedientes.Find(id) ?? throw new RepositorioException("No existe");
        _context.Expedientes.Remove(e);
    }

     public void Modificar(Expediente expediente)
    {
        
    }
}