using System;
using SGE.Dominio.Expedientes; // Para reconocer EstadoExpediente

namespace SGE.Aplicacion.Expedientes;

public record class CambiarEstadoExpedienteRequest(
    Guid Id, 
    EstadoExpediente NuevoEstado, 
    Guid UsuarioId);