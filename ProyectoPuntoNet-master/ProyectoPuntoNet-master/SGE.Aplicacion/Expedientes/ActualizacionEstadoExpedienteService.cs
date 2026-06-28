namespace SGE.Aplicacion.Expedientes;

using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Aplicacion.Tramites;
using System.Linq;

public class ActualizacionEstadoExpedienteService(IExpedienteRepository expedienteRepo, ITramiteRepository tramiteRepo)
{
    public void Ejecutar(Guid expedienteId, Guid usuarioId)
    {
        var expediente = expedienteRepo.ObtenerPorId(expedienteId);
        if (expediente == null) return;

        // obtener tramites y buscar el ultimo
        var tramites = tramiteRepo.ObtenerPorExpedienteId(expedienteId);
        var ultimoTramite = tramites.OrderByDescending(t => t.FechaCreacion).FirstOrDefault();

        // pedir a entidad que actualice estado
        // el EF core detecta el cambio automaticamente
        expediente.ActualizarEstado(ultimoTramite?.Etiqueta, usuarioId);

        // no llamo ni modificar ni guardar ya que el caso de uso que invoco este metodo se ocupa de eso

    }
}