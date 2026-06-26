using SGE.Dominio.Tramites;
using SGE.Infraestructura.Persistencia;
using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Tramites;

namespace SGE.Infraestructura.Repositorios;

public class TramiteRepository(SgeContext context) : ITramiteRepository
{
    public void Agregar(Tramite t) => context.Tramites.Add(t);
    public Tramite? ObtenerPorId(Guid id) => context.Tramites.Find(id);
    public IEnumerable<Tramite> ObtenerTodos() => context.Tramites;
    public void Eliminar(Guid id)
    {
        var t = context.Tramites.Find(id) ?? throw new RepositorioException("No existe");
        context.Tramites.Remove(t);
    }

   public void Modificar(Tramite tramite) { } // el change tracker se encargara

    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        return context.Tramites.Where(t => t.ExpedienteId == expedienteId);
    }
}