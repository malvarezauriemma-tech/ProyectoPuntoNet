namespace SGE.Dominio.Tramites;
using System;
using SGE.Dominio.Comun;

public class Tramite
{
    public Guid Id { get; private set; }

    public Guid ExpedienteId { get; private set; }

    public EtiquetaTramite Etiqueta { get; private set; }

    public ContenidoTramite Contenido { get; private set; }

    public DateTime FechaCreacion { get; private set; }

    public DateTime FechaUltimaModificacion { get; private set; }

    public Guid UsuarioUltimoCambio { get; private set; }

    // constructor privado para reconstruccion
    public Tramite(Guid expedienteId, EtiquetaTramite etiqueta, ContenidoTramite contenido, DateTime fechaCreacion, DateTime fechaModif, Guid usuarioId)
    {
        if (expedienteId == Guid.Empty)
        {
            throw new DominioException("ExpedienteID obligatorio");
        }
        if (usuarioId == Guid.Empty)
        {
            throw new DominioException("Usuario obligatorio");
        }
        if (fechaModif < fechaCreacion)
        {
            throw new DominioException("Fecha de modificacion inválida");
        }

        Id = Guid.NewGuid();
        ExpedienteId = expedienteId;
        Etiqueta = etiqueta;
        Contenido = contenido ?? throw new DominioException("El contenido es obligatorio");
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaModif;
        UsuarioUltimoCambio = usuarioId;
    }

    protected Tramite() {}

    // metodo para poder modificar el contenido con los casos de uso 
    public void Modificar(EtiquetaTramite nuevaEtiqueta, ContenidoTramite nuevoContenido, Guid usuarioId)
    {
        if (usuarioId == Guid.Empty)
        {
            throw new DominioException("Usuario de modificacion invalido");
        }
        Etiqueta = nuevaEtiqueta;
        Contenido = nuevoContenido ?? throw new DominioException("El contenido es obligatorio");

        // actualizar datos
        UsuarioUltimoCambio = usuarioId;
        FechaUltimaModificacion = DateTime.Now;
    }
}