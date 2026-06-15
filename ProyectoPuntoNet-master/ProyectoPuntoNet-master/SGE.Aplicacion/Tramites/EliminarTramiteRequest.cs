namespace SGE.Aplicacion.Tramites;

public record class EliminarTramiteRequest(Guid TramiteId, Guid UsuarioId);