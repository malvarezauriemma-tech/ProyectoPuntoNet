using System;
using System.Collections.Generic;
using System.Linq;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion;

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
            $"{expediente.Caratula.Valor};" +
            $"{expediente.FechaCreacion};" +
            $"{expediente.FechaUltimaModificacion};" +
            $"{expediente.UsuarioUltimoCambio};" +
            $"{expediente.Estado}";

        // Agrega esa línea al archivo
        File.AppendAllLines(_rutaArchivo, new[] { linea });
    }


    // ACOMODAR A PARTIR DE ACA
    // OBTENER TODOS
    public IEnumerable<Expediente> ObtenerTodos()
    {
        // Lista para guardar los expedientes
        var expedientes = new List<Expediente>();


        // Si el archivo no existe todavía,
        // devuelve lista vacía
        if (!File.Exists(_rutaArchivo))
        {
            return expedientes;
        }


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


    // OBTENER POR ID
    public Expediente? ObtenerPorId(Guid id)
    {
        return ObtenerTodos().FirstOrDefault(x => x.Id == id);
    }


    // MODIFICAR EXPEDIENTE
    public void Modificar(Expediente expediente)
    {
        var expedientes = ObtenerTodos().ToList();
        var index = expedientes.FindIndex(x => x.Id == expediente.Id);
        if (index < 0)
        {
            throw new InvalidOperationException("Expediente no encontrado");
        }

        expedientes[index] = expediente;
        GuardarTodos(expedientes);
    }


    // ELIMINAR EXPEDIENTE
    public void Eliminar(Guid id)
    {
        var expedientes = ObtenerTodos().ToList();
        var expedientesFiltrados = expedientes.Where(x => x.Id != id).ToList();
        GuardarTodos(expedientesFiltrados);
    }


    private void GuardarTodos(IEnumerable<Expediente> expedientes)
    {
        var lineas = expedientes.Select(expediente =>
            $"{expediente.Id};" +
            $"{expediente.Caratula.Valor};" +
            $"{expediente.FechaCreacion};" +
            $"{expediente.FechaUltimaModificacion};" +
            $"{expediente.UsuarioUltimoCambio};" +
            $"{expediente.Estado}"
        );

        File.WriteAllLines(_rutaArchivo, lineas);
    }
}