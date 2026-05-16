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
        bool huboCambio = expediente.ActualizarEstado(ultimoTramite?.Etiqueta, usuarioId);

        // si cambio el estado, lo guardo al expediente modificado
        if (huboCambio)
        {
            expedienteRepo.Modificar(expediente);
        }
    }
}