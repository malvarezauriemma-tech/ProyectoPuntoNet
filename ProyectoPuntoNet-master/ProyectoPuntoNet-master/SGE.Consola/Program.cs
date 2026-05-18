// Aplicacion
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Excepcion;

// Dominio
using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;

// Infraestructura
using SGE.Infraestructura.Expedientes;
using SGE.Infraestructura.Tramites;
using SGE.Infraestructura.Autorizacion; 


// ==========================================================================

// Acá se crean las implementaciones
var expedienteRepository = new ExpedienteTxtRepository();
var tramiteRepository = new TramiteTxtRepository();
var autorizacionService = new AutorizacionProvisionalService();

// instancio tambien el actualizador estado
var actualizadorEstado = new ActualizacionEstadoExpedienteService(expedienteRepository, tramiteRepository);

// ============================================================================
// Se les inyectan las dependencias por constructor
var agregarExpedienteUseCase = new AgregarExpedienteUseCase(expedienteRepository, autorizacionService);

var agregarTramiteUseCase = new AgregarTramiteUseCase(tramiteRepository, expedienteRepository, autorizacionService, actualizadorEstado);

var listarExpedientesUseCase = new ListarTodosLosExpedientesUseCase(expedienteRepository);

var listarTramitesUseCase = new ListarTramitesPorExpedienteUseCase(tramiteRepository);


// =============================================================================

try
{
    Console.WriteLine("=== CREANDO EXPEDIENTE ===");

    // DTO para crear expediente
    var expedienteRequest = new AgregarExpedienteRequest("Expediente de prueba",Guid.NewGuid());


    // Ejecuta caso de uso
    agregarExpedienteUseCase.Ejecutar(expedienteRequest);

    Console.WriteLine("Expediente creado correctamente");

    Console.WriteLine("\n=== LISTANDO EXPEDIENTES ===");

    // Uso el caso de uso para obtener DTOs, no entidades
    var expedientesResponse = listarExpedientesUseCase.Ejecutar(new ListarTodosLosExpedientesRequest());

    foreach (var expediente in expedientesResponse.Expedientes)
    {
        Console.WriteLine($"{expediente.Id} |  {expediente.Caratula} |  {expediente.Estado}");
    }

    Console.WriteLine("\n=== AGREGANDO TRÁMITE ===");

    // Toma el primer expediente
    var primerExpediente = expedientesResponse.Expedientes.First();

    // DTO para trámite
    var tramiteRequest = new AgregarTramiteRequest(primerExpediente.Id, EtiquetaTramite.Resolucion, "Se resolvió el expediente", Guid.NewGuid());


    // Ejecuta caso de uso
    agregarTramiteUseCase.Ejecutar(tramiteRequest);

    Console.WriteLine("Trámite agregado correctamente");


    Console.WriteLine("\n=== LISTANDO TRÁMITES DEL EXPEDIENTE ===");

    var tramitesResponse = listarTramitesUseCase.Ejecutar(new ListarTramitesPorExpedienteRequest(primerExpediente.Id));

    foreach (var tramite in tramitesResponse.Tramites)
    {
        Console.WriteLine($"{tramite.Id} | {tramite.Etiqueta} |  {tramite.Contenido}");
    }
}

// Captura de errores 
catch (DominioException ex)
{
    Console.WriteLine($"Error de dominio: {ex.Message}");
}

catch (RepositorioException ex)
{
    Console.WriteLine($"Error de respositorio: {ex.Message}");
}

catch (AutorizacionException ex)
{
    Console.WriteLine($"Error de autorización: {ex.Message}");
}

catch (Exception ex)
{
    Console.WriteLine($"Error general: {ex.Message}");
} 

