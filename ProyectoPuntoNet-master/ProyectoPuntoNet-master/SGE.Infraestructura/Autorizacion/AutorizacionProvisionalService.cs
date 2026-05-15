using System;

namespace SGE.Infraestructura.Autorizacion;

// Esta clase IMPLEMENTA la interfaz IAutorizacionService
public class AutorizacionProvisionalService : IAutorizacionService
{
    // Método exigido por la interfaz
    public bool PoseeElPermiso(Guid idUsuario, Permiso permiso)
    {
        // Por ahora siempre devuelve true
        // Más adelante podría consultar base de datos
        return true;
    }
}