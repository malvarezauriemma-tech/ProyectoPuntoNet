using SGE.Aplicacion.Expedientes;

using SGE.Dominio.Expedientes;

namespace SGE.Infraestructura.Expedientes;

public class ExpedienteTxtRepository : IExpedienteRepository
{
    // Ruta del archivo donde se guardarán los expedientes
    private readonly string _rutaArchivo = "expedientes.txt";




    // AGREGAR EXPEDIENTE
    public void Agregar(Expediente expediente)
    {
        // Convierte el expediente en una línea de texto
        // usando ; como separador
        string linea =
            $"{expediente.Id};" +
            $"{expediente.Caratula.Texto};" +
            $"{expediente.FechaCreacion};" +
            $"{expediente.FechaUltimaModificacion};" +
            $"{expediente.UsuarioUltimoCambio};" +
            $"{expediente.Estado}";

        // Agrega esa línea al archivo
        File.AppendAllLines(_rutaArchivo, new[] { linea });
    }



    // OBTENER TODOS
    public List<Expediente> ObtenerTodos()
    {
        // Lista para guardar los expedientes
        var expedientes = new List<Expediente>();


        // Si el archivo no existe todavía,
        // devuelve lista vacía
        if (!File.Exists(_rutaArchivo))
        {
            return expedientes;
        }


        // ??????
        var lineas = File.ReadAllLines(_rutaArchivo);

        foreach (var linea in lineas)
        {
            var datos = linea.Split(';');


            // Reconstruye el objeto Expediente
            var expediente = Expediente.Reconstruir(
                Guid.Parse(datos[0]),
                new Caratula(datos[1]),
                DateTime.Parse(datos[2]),
                DateTime.Parse(datos[3]),
                Guid.Parse(datos[4]),
                Enum.Parse<EstadoExpediente>(datos[5])
            );


            expedientes.Add(expediente);
        }

        return expedientes;
    }
}