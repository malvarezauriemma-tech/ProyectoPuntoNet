using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Tramites;

namespace SGE.Aplicacion.Expedientes;

public class ObtenerExpedientePorIdUseCase(IExpedienteRepository expedienteRepo, ITramiteRepository tramiteRepo)
{
    public ObtenerExpedientePorIdResponse Ejecutar(ObtenerExpedientePorIdRequest request)
    {
        // busco expediente
        var expediente = expedienteRepo.ObtenerPorId(request.expedienteId);
        if (expediente == null)
        {
            throw new EntidadNoEncontradaException("Expediente no encontrado");
        }

        // busco todos sus tramites asociados
        var tramites = tramiteRepo.ObtenerPorExpedienteId(request.expedienteId);

        // mapeo tramites a DTOs
        var tramitesDTOs = tramites.Select(t => new TramiteDTO(t.Id, t.ExpedienteId, t.Etiqueta.ToString(), t.Contenido.Valor, t.FechaCreacion)).ToList();

        // armo DTO de detalle completo
        var detalle = new ExpedienteDetalleDTO(expediente.Id, expediente.Caratula.Valor, expediente.Estado.ToString(), expediente.FechaCreacion, tramitesDTOs);

        return new ObtenerExpedientePorIdResponse(detalle);
    }
}