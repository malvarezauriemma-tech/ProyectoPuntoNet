using System;
namespace SGE.Aplicacion.Expedientes;

public record class ModificarCaratulaRequest(Guid Id, string NuevaCaratula, Guid UsuarioId);