using SGE.Dominio.Comun;

namespace SGE.Dominio.Tramites;

public record ContenidoTramite
{
    public string Valor { get; init;} = "";

    public ContenidoTramite(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new DominioException("El contenido del trámite no puede estar vacío");
        }

        Valor = texto;
    }

    protected ContenidoTramite() {}
}