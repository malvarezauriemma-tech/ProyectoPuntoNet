using System;

namespace SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Excepcion;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Autorizacion;

public class EliminarExpedienteUseCase(IExpedienteRepository expedienteRepo, ITramiteRepository repositorio, 
    IAutorizacionService autorizacionService)
{
    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest request)
    {
        
    }
}