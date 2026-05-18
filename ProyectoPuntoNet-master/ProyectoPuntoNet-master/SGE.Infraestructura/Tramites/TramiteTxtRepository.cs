using System;
using System.IO; 
using System.Collections.Generic; 
using System.Linq;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;
using SGE.Aplicacion.Excepcion;


namespace SGE.Infraestructura.Tramites;


public class TramiteTxtRepository : ITramiteRepository
{
    // Archivo donde se guardarán los trámites
    private readonly string _rutaArchivo = "tramites.txt";

    // AGREGAR TRÁMITE
    public void Agregar(Tramite tramite)
    { 
        string linea = FormatearLinea(tramite);

        // Agrega la línea al archivo
        File.AppendAllLines(_rutaArchivo, [linea]);
    }


    // OBTENER TODOS
    public List<Tramite> ObtenerTodos()
    {
        var tramites = new List<Tramite>();

        // otra vez uso esto para saber si existe el archivo
        if (!File.Exists(_rutaArchivo))
        {
            return tramites;
        }

        //Otra vez uso el readalllines
        var lineas = File.ReadAllLines(_rutaArchivo);

        foreach (var linea in lineas)
        {
            var datos = linea.Split(';');

            // Reconstruye el trámite desde el texto
            var tramite = Tramite.Reconstruir(
            Guid.Parse(datos[0]),
            Guid.Parse(datos[1]),
            Enum.Parse<EtiquetaTramite>(datos[2]),
            new ContenidoTramite(datos[3]),
            DateTime.Parse(datos[4]),
            DateTime.Parse(datos[5]),
            Guid.Parse(datos[6])
            );
            tramites.Add(tramite);
        }
        return tramites;
    }


    public void Modificar(Tramite tramite)
    {
        var tramites = ObtenerTodos();
        int indice = tramites.FindIndex(t => t.Id == tramite.Id);

        if (indice == -1)
        {
            throw new RepositorioException($"No se encontro el tramite con ID {tramite.Id}");
        }

        tramites[indice] = tramite;
        GuardarListaCompleta(tramites);
    }

    public void Eliminar(Guid id)
    {
        var tramites = ObtenerTodos();
        var tramiteAEliminar = tramites.FirstOrDefault(t => t.Id == id);

        if (tramiteAEliminar == null)
        {
            throw new RepositorioException($"No se puede eliminar, no existe el tramite con ID: {id}");
        }

        tramites.Remove(tramiteAEliminar);
        GuardarListaCompleta(tramites);
    }

    public Tramite? ObtenerPorId(Guid id)
    {
        // retornamos la entidad o null si no se halla
        return ObtenerTodos().FirstOrDefault(t => t.Id == id);
    }

    public List<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        return ObtenerTodos().Where(t => t.ExpedienteId == expedienteId).ToList();
    }

    // metodos que nos sirven
    private void GuardarListaCompleta(List<Tramite> tramites)
    {
        // sobreescribimos el archivo completo 
        var lineas = tramites.Select(FormatearLinea);
        File.WriteAllLines(_rutaArchivo, lineas);
    }

    private string FormatearLinea(Tramite t)
    {
        return $"{t.Id};{t.ExpedienteId};{t.Etiqueta};{t.Contenido.Texto};{t.FechaCreacion};{t.FechaUltimaModificacion};{t.UsuarioUltimoCambio}";
    }
}


