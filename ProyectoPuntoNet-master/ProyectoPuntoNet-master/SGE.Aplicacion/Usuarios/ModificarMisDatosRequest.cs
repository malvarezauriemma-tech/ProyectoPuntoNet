using System;

namespace SGE.Aplicacion.Usuarios;

public record class ModificarMisDatosRequest(Guid Id = default, string nuevoNombre = "", string nuevaPassword = "");