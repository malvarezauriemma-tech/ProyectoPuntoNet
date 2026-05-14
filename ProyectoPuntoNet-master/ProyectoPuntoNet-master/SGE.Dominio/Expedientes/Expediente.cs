namespace SGE.Dominio.Expedientes;
using SGE.Dominio.Expediente;
public class Expediente
{
    public Guid Id {get ; private set;}
    public Caratula Caratula {get; private set;} // value object
    public DateTime FechaCreacion {get; private set;}
    public DateTime FechaUltimaModificacion {get; private set;}
    public Guid UsuarioUltimoCambio {get; private set;}
    public EstadoExpediente Estado {get; private set;} // enumerativo

    public Expediente(Caratula caratula, Guid usuarioID)
    {
        Id = Guid.NewGuid();
        Caratula = caratula;
        UsuarioUltimoCambio = usuarioID;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = DateTime.Now;
        Estado = EstadoExpediente.RecienIniciado;
    }

    // constructor privado para la reconstruccion
    private Expediente(Guid id, Caratula caratula, DateTime fechaCreacion, DateTime fechaUltMod, Guid usuarioID, EstadoExpediente estado)
    {
        Id =id;
        Caratula = caratula;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltMod;
        UsuarioUltimoCambio = usuarioID;
        Estado = estado;
    }

    // metodo publico para usar la capa de infraestructura para reconstruir 
    public static Expediente Reconstruir(Guid id, Caratula caratula, DateTime fechaCreacion, DateTime fechaUltMod, Guid usuarioID, EstadoExpediente estado)
    {
        return new Expediente(id, caratula, fechaCreacion, fechaUltMod, usuarioID, estado);
    }
}