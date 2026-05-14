namespace SGE.Aplicacion.Excepcion;

public class AutorizacionException : Exception
{
    public AutorizacionException(string mensaje) : base(mensaje)
    {
    }
}