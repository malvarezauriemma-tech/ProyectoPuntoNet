using System;
using SGE.WebApi;
using SGE.Aplicacion;
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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // documentación interactica en scalar
}

// mapeo de rutas
app.MapGet("/", () => "SGE API Funcionando");
app.MapExpedientesEndpoints();
// app.MapTramitesEndpoints();
// app.MapUsuariosEndpoints();

app.Run();
