using System;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;

public record ModificarPermisosUsuarioRequest(Permiso permiso, bool Asignar, Guid UsuarioId = default, Guid IdEjecutor = default);