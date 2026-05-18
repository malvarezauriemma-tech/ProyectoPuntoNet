namespace SGE.Aplicacion.Tramites;

// recibe ID de expediente para poder filtralo
public record class ListarTramitesPorExpedienteRequest(Guid ExpedienteId);