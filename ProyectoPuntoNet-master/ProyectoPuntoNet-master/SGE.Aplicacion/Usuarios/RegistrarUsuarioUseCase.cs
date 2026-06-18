using SGE.Aplicacion.Excepcion;
using SGE.Dominio.Usuarios;
using System.Security.Cryptography;
using System.Text;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Aplicacion.Usuarios;

public class RegistrarUsuarioUseCase(IUsuarioRepository repositorio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        // validamos que el correo no este repetido
        var usuarioExistente = repositorio.ObtenerPorEmail(request.correo);
        if (usuarioExistente != null)
        {
            throw new EntidadNoEncontradaException("El correo electronico ya se encuentra registrado");
        }

        // cifrar contraseña
        string contrasenaHasheada = HashPassword(request.password);

        // creo entidad
        var nuevoUsuario = new Usuario(request.nombre, request.correo, contrasenaHasheada, esAdmin: false);

        repositorio.Agregar(nuevoUsuario);
        unidadDeTrabajo.Guardar();

        return new RegistrarUsuarioResponse(nuevoUsuario.Id);
    }

    // metodo para cumplir el hash
    private string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password); // tomo contraseña que ingreso usuario, y lo traduce a una secuencia de bytes
        var hash = SHA256.HashData(bytes);  // toma los bytes y les aplica una serie de operaciones matematicas y genera un resumen de longitud fija de 256 bits 
        return Convert.ToHexString(hash); // convierte estos bytes recientes en una cadena de texto hexadecimal, asi el hash transforma un string legible que se asigna a la propiedad ContrasenaHash
    }
}