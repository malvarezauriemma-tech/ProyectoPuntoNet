using System;

namespace SGE.Aplicacion;

public interface IAutorizacionService
{
    bool PoseeElPermiso(Guid idUsuario, Permiso permiso);
}
