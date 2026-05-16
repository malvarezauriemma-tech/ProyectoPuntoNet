namespace SGE.Aplicacion.Tramites;

public record class TramiteDTO(Guid Id, Guid ExpedienteId, string Etiqueta, string Contenido, DateTime FechaCreacion);