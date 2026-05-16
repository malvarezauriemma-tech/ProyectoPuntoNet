namespace SGE.Aplicacion.Expedientes;

public record class ExpedienteDTO(Guid Id, string Caratula, string Estado, DateTime FechaCreacion);
