namespace SGE.Dominio.Expedientes;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Dominio.Comun;
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

    // modificar caratula: usuario cometio error al ingresar el expediente y debe corregirla. Actualiza el dato
    // , el usuario responsable y la fecha de modificación
    public void ModificarCaratula(Caratula nuevaCaratula, Guid idUsuario)
    {
        if (nuevaCaratula == null)
        {
            throw new DominioException("La caratula es obligatoria");
        }
        this.Caratula = nuevaCaratula;
        this.UsuarioUltimoCambio = idUsuario;
        this.FechaUltimaModificacion = DateTime.Now;
    }

    // actualizar estado automatico: retorna true si el estado efectivamente cambio y dependiendo la etiqueta se le
    // asigna el estado correspondiente segun lo pedido

    public bool ActualizarEstado(EtiquetaTramite? ultimaEtiqueta, Guid idUsuario)
    {
        EstadoExpediente estadoAnterior = this.Estado;

        switch (ultimaEtiqueta)
        {
            case EtiquetaTramite.Resolucion: 
                this.Estado = EstadoExpediente.ConResolucion;
                break;
            case EtiquetaTramite.PaseAEstudio:
                this.Estado = EstadoExpediente.ParaResolver;
                break;
            case EtiquetaTramite.PaseAlArchivo:
                this.Estado = EstadoExpediente.Finalizado;
                break;
            case null: // no hay Tramites
                this.Estado = EstadoExpediente.RecienIniciado;
                break;
            default:
                break;
        }

        if (this.Estado != estadoAnterior)
        {
            this.UsuarioUltimoCambio = idUsuario;
            this.FechaUltimaModificacion = DateTime.Now;
            return true;
        }
        return false;
    }
    
    // cambio de estado maual: recibo el nuevo estado y actualizo la información correspondiente
    public void CambiarEstado(EstadoExpediente nuevoEstado, Guid idUsuario)
    {
        this.Estado = nuevoEstado;
        this.UsuarioUltimoCambio = idUsuario;
        this.FechaUltimaModificacion = DateTime.Now;
    }
}