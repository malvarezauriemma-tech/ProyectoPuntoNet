using SGE.Aplicacion.Excepcion;
using SGE.Dominio.Usuarios;
using System.Security.Cryptography;
using System.Text;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Comun;

namespace SGE.Aplicacion.Usuarios;

public class RegistrarUsuarioUseCase(IUsuarioRepository repositorio, IUnidadDeTrabajo unidadDeTrabajo, IHashService hashService)
{
    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        // validamos que el correo no este repetido
        var usuarioExistente = repositorio.ObtenerPorEmail(request.correo);
        if (usuarioExistente != null)
        {
            throw new DominioException("El correo electronico ya se encuentra registrado");
        }

        // cifrar contraseña
        string contrasenaHasheada = hashService.ObtenerHash(request.password);

        // creo entidad
        var nuevoUsuario = new Usuario(request.nombre, request.correo, contrasenaHasheada, esAdmin: false);

        repositorio.Agregar(nuevoUsuario);
        unidadDeTrabajo.Guardar();

        return new RegistrarUsuarioResponse(nuevoUsuario.Id);
    }
}