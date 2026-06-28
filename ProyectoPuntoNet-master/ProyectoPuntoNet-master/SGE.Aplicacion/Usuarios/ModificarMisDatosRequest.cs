using System;

namespace SGE.Aplicacion.Usuarios;

public record class ModificarMisDatosRequest(Guid Id, string nuevoNombre, string nuevaPassword);