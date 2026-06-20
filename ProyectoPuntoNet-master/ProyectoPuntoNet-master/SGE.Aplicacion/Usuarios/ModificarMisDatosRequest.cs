using System;

namespace SGE.Aplicacion.Usuarios;

public record class ModificarMisDatosRequest(Guid IdUsuario, string nuevoNombre, string nuevaPassword);