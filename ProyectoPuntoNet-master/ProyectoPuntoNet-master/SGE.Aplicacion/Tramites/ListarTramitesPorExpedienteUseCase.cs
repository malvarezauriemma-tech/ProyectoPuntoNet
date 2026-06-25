namespace SGE.Aplicacion.Tramites;

public class ListarTramitesPorExpedienteUseCase(ITramiteRepository repo)
{
    public ListarTramitesPorExpedienteResponse Ejecutar(ListarTramitesPorExpedienteRequest request)
    {
        var tramites = repo.ObtenerPorExpedienteId(request.ExpedienteId);

        // mapeo  adto (no exponemos dominio)
        var dtos = tramites.Select(t => new TramiteDTO(t.Id, t.ExpedienteId, t.Etiqueta.ToString(), t.Contenido.Valor, t.FechaCreacion));

        return new ListarTramitesPorExpedienteResponse(dtos);
    }
}