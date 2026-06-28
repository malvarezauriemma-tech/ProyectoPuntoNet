namespace SGE.Aplicacion.Abstracciones;

public interface IHashService
{
    // recibe el texto plano y devuelve el hash
    string ObtenerHash(string textoPlano);
}