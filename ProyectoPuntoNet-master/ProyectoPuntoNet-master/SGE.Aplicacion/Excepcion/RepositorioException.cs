namespace SGE.Aplicacion.Excepcion;

public class RepositorioException : Exception
{
    public RepositorioException(string mensaje) : base(mensaje)
    {
    }
}