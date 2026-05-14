namespace SGE.Infraestructura.Comun;

// Excepción personalizada para errores relacionados con repositorios
public class RepositorioException : Exception
{
    // Constructor que recibe un mensaje y se lo pasa a Exception
    public RepositorioException(string mensaje) : base(mensaje)
    {
    }
}