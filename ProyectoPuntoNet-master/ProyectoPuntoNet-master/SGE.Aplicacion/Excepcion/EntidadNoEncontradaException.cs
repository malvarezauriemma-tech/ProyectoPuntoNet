namespace SGE.Aplicacion.Excepcion;

public class EntidadNoEncontradaException : Exception
{
    public EntidadNoEncontradaException(string mensaje) : base(mensaje)
    {
    }
}