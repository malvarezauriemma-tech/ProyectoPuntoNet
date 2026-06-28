using Microsoft.EntityFrameworkCore;
using SGE.Infraestructura.Persistencia;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Infraestructura;

public static class SgeSqlite 
{
    public static void Inicializar(SgeContext context, IHashService hashService)
    {
        // intento crear la base de datos, si no existe se crea segun el modelo y devuelve true
        if (context.Database.EnsureCreated())
        {
            // optimizo sqlite con el modo delete para consistencia inmediata
            var connection = context.Database.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA journal_mode=DELETE;";
                command.ExecuteNonQuery();
            }

            // carga de datos de semilla: las contraseñas tienen que estar hasheadas

            // admin semilla:
            var admin = new Usuario("Admin del sistema", 
                "admin@sge.com",
                hashService.ObtenerHash("admin123"), // hash de 'admin123'
                true
            );

            // usuario con menos permisos (no es admin)
            var usuarioOperador = new Usuario("Usuario operador",
                "operador@sge.com",
                hashService.ObtenerHash("operador123"),
                false
            );

            // asigno los permisos:
            usuarioOperador.AsignarPermiso(Permiso.ExpedienteAlta);
            usuarioOperador.AsignarPermiso(Permiso.TramiteAlta);

            // usuario de solo lectura (sin permisos)
            var usuarioConsulta = new Usuario("Usuario consulta",
                "lector@sge.com",
                hashService.ObtenerHash("lector123"),
                false
            );

            // guardo todo en la base de datos
            context.Usuarios.AddRange(admin, usuarioOperador, usuarioConsulta);
            context.SaveChanges();
        }
    }
}