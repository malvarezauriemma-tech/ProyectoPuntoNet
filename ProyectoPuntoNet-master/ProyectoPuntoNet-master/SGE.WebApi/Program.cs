using System;
using SGE.WebApi.Endpoints;
using SGE.WebApi;
using SGE.Aplicacion;
using SGE.Infraestructura.Persistencia;
using SGE.Infraestructura;
using Scalar.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// registro de servicios
builder.Services.AddOpenApi()
    .AddAplicacion() // registra use cases
    .AddInfraestructura(builder.Configuration)
    .AddSeguridadJwt(builder.Configuration); // registro de token provider y jwt
builder.Services.AddProblemDetails(); // requerido para el manejador de errores
builder.Services.AddExceptionHandler<ManejadorDeExcepcionesGlobales>();


var app = builder.Build();

// configuracion del pipeline (middlewares)
app.UseExceptionHandler();

app.UseAuthentication(); // "descubre quién es" leyendo el token 
app.UseAuthorization();  // "decide si tiene permiso" 

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // documentación interactica en scalar
}

// mapeo de rutas
app.MapGet("/", () => "SGE API Funcionando");
app.MapExpedientesEndpoints();
app.MapTramitesEndpoints();
app.MapUsuariosEndpoints();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SgeContext>();
    // Aquí llamamos a la clase que crea la base y carga los datos semilla [9]
    SgeSqlite.Inicializar(context); 
}

app.Run();
