using System;

namespace SGE.Aplicacion.Expedientes;

public record class AgregarExpedienteRequest(string caratula, Guid usuarioID);

