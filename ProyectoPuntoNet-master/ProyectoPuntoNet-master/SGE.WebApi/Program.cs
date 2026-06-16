var builder = WebApplication.CreateBuilder(args);

// 1. Extraemos la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("SgeDb");

// 2. Configuramos Entity Framework para usar SQLite
// (Para esto instalamos el paquete Microsoft.EntityFrameworkCore.Sqlite)
builder.Services.AddDbContext<SgeContext>(options => 
    options.UseSqlite(connectionString));

// Aquí irán los registros de tus Repositorios y Casos de Uso más adelante...

var app = builder.Build();

// Antes de los endpoints, configuramos los "peajes" o filtros
app.UseExceptionHandler(); // Atrapa errores automáticamente [5]
app.UseAuthentication();   // Verifica quién es el usuario [6]
app.UseAuthorization();    // Verifica qué permisos tiene [6]

app.MapGet("/", () => "¡La API del Sistema de Gestión de Expedientes está funcionando!");

// Ejemplo de un endpoint para listar expedientes
app.MapGet("/api/expedientes", (ListarExpedientesUseCase useCase) => 
    Results.Ok(useCase.Ejecutar()));