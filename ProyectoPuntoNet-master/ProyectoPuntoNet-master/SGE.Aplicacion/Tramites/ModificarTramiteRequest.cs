using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public record class ModificarTramiteRequest(Guid Id, EtiquetaTramite NuevaEtiqueta, string nuevoContenido, Guid UsuarioId);