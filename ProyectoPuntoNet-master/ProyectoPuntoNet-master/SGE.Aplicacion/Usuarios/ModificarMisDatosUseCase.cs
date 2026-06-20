using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;
using System.Security.Cryptography;
using System.Text;

namespace SGE.Aplicacion.Usuarios;

public class ModificarMisDatosUseCase(IUsuarioRepository repositorio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public ModificarMisDatosResponse Ejecutar(ModificarMisDatosRequest request)
    {
        // busco el usuario en kla base de datos por el ID que viene del token
        var usuario = repositorio.ObtenerPorId(request.IdUsuario);
        if (usuario == null)
        {
            throw new Exception("Usuario no encontrado");
        }

        // cifro la nueva contraseña con el hash
        string nuevaContraseñaHash = HashPassword(request.nuevaPassword);

        // entidad valida sus propias reglas y actualizo
        usuario.ActualizarDatos(request.nuevoNombre, nuevaContraseñaHash);

        // el EF Core detecta el cambio automaticamente, solo llamo a guardar
        unidadDeTrabajo.Guardar();

        return new ModificarMisDatosResponse();
    }

    private string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash);
    }
}