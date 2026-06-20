using System;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;

public record ModificarPermisosUsuarioRequest(Guid IdAdmin, Guid IdUsuarioObjetivo, Permiso permiso, bool Asignar);