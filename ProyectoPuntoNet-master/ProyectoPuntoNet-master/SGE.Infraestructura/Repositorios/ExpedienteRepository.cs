using SGE.Dominio.Expedientes;
using SGE.Infraestructura.Persistencia;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Excepcion;

namespace SGE.Infraestructura.Repositorios;

public class ExpedienteRepository(SgeContext context) : IExpedienteRepository
{
    public void Agregar(Expediente e) => context.Expedientes.Add(e);
    public Expediente? ObtenerPorId(Guid id) => context.Expedientes.Find(id);
    public IEnumerable<Expediente> ObtenerTodos() => context.Expedientes;
    
    public void Eliminar(Guid id)
    {
        var e = context.Expedientes.Find(id) ?? throw new RepositorioException($"No existe el expediente con ID {id}");
        context.Expedientes.Remove(e);
    }

     public void Modificar(Expediente expediente)
    {
        // el change tracker se encargara
    }
}