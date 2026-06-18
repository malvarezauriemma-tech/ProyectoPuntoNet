using SGE.Dominio.Usuarios;
using System.Security.Cryptography;
using System.Text;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;

namespace SGE.Aplicacion.Usuarios;

public class LoginUseCase(IUsuarioRepository repositorio, ITokenProvider tokenProvider)
{
    public LoginResponse Ejecutar(LoginRequest request)
    {
        // busco usuario por email (identificador unico)
        var usuario = repositorio.ObtenerPorEmail(request.email);

        // recaulculo hash de contraseña ingresada, asi comparo
        string hashIngresado = HashPassword(request.password);

        if (usuario == null || usuario.ContrasenaHash != hashIngresado)
        {
            throw new AutorizacionException("Email o contraseña incorrectos");
        }

        // genero token y delego tarea a la abstraccion
        var token = tokenProvider.GenerarToken(usuario);

        return new LoginResponse(token);
    }

    // metodo para garantizar que el hash sea identico
    private string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password); // tomo contraseña que ingreso usuario, y lo traduce a una secuencia de bytes
        var hash = SHA256.HashData(bytes);  // toma los bytes y les aplica una serie de operaciones matematicas y genera un resumen de longitud fija de 256 bits 
        return Convert.ToHexString(hash); // convierte estos bytes recientes en una cadena de texto hexadecimal, asi el hash transforma un string legible que se asigna a la propiedad ContrasenaHash
    }
}