using System;
namespace SGE.Aplicacion.Expedientes;
public record class EliminarExpedienteRequest(Guid ExpedienteId, Guid IdUsuario);