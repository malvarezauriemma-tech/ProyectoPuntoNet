namespace SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;

public record ExpedienteDetalleDTO(Guid Id, string Caratula, string Estado, DateTime FechaCreacion, List<TramiteDTO> Tramites);