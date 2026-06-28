using SGE.Dominio.Usuarios;
using System.Security.Cryptography;
using System.Text;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Autorizacion;

namespace SGE.Aplicacion.Usuarios;

public class LoginUseCase(IUsuarioRepository repositorio, ITokenProvider tokenProvider, IHashService hashService)
{
    public LoginResponse Ejecutar(LoginRequest request)
    {
        // busco usuario por email (identificador unico)
        var usuario = repositorio.ObtenerPorEmail(request.email);

        // recaulculo hash de contraseña ingresada, asi comparo
        string hashIngresado = hashService.ObtenerHash(request.password);

        if (usuario == null || usuario.ContrasenaHash != hashIngresado)
        {
            throw new AutorizacionException("Email o contraseña incorrectos");
        }

        // genero token y delego tarea a la abstraccion
        var token = tokenProvider.GenerarToken(usuario);

        return new LoginResponse(token);
    }
}