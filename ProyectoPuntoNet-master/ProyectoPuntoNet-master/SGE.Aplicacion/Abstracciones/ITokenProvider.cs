using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Abstracciones;

public interface ITokenProvider
{
    string GenerarToken(Usuario usuario);
}